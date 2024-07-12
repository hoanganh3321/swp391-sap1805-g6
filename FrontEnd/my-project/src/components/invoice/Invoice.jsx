import React, { useState, useEffect, useRef } from "react";
import {
  Row,
  Col,
  Table,
  Modal,
  Form,
  Input,
  Button,
  Popconfirm,
  message,
  DatePicker,
  InputNumber,
  Checkbox,
} from "antd";
import { ExportOutlined } from "@ant-design/icons";
import Card from "../../components/card";
import { commonAPI } from "../../api/common.api";
import { toPng } from "html-to-image";
import Footer from "../footer/FooterHomePage";
import Navbar from "../navbar/Navbar";

const Invoice = () => {
  const [Customer, setCustomer] = useState([]);
  const [loading, setLoading] = useState(true);
  const [searchId, setSearchId] = useState("");
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [editData, setEditData] = useState(null);
  const [isModalOpenReturn, setIsModalOpenReturn] = useState(false);
  const invoiceRef = useRef();
  const [invoiceData, setInvoiceData] = useState({});

  useEffect(() => {
    fetchCustomer();
  }, []);

  const fetchCustomer = async () => {
    try {
      const response = await commonAPI.getAPI("staffOrder/listallinvoice");
      setCustomer(response.data);
      setLoading(false);
    } catch (error) {
      console.error("Error fetching Customer:", error);
      setLoading(false);
    }
  };

  const handleSearch = async () => {
    try {
      const token = localStorage.getItem("token");
      if (!token) {
        throw new Error("No token found. Please log in.");
      }

      if (searchId.trim() === "") {
        fetchCustomer();
      } else {
        const response = await commonAPI.getAPI(
          `staffOrder/searchInvoice/${searchId}`
        );
        setCustomer([response.data]);
        setLoading(false);
      }
    } catch (error) {
      setCustomer([]);
      console.error("Error searching Customer by ID:", error);
      setLoading(false);
    }
  };

  const columns = [
    {
      title: "InvoiceID",
      dataIndex: "invoiceId",
      key: "invoiceId",
    },
    {
      title: "OrderID",
      dataIndex: "orderId",
      key: "orderId",
    },
    {
      title: "PromotionID",
      dataIndex: "promotionId",
      key: "promotionId",
    },
    {
      title: "Customer First Name",
      dataIndex: ["order", "customer", "firstName"],
      key: "customerFirstName",
    },
    {
      title: "PromotionName",
      dataIndex: "promotionName",
      key: "promotionName",
    },
    {
      title: "TotalPrice",
      dataIndex: "totalPrice",
      key: "totalPrice",
    },
    {
      title: "StaffID",
      dataIndex: "staffId",
      key: "staffId",
    },
    {
      title: "Action",
      dataIndex: "action",
      key: "action",
      align: "center",
      render: (text, record) => (
        <span style={{ display: "flex", justifyContent: "center" }}>
          <Button
            type="link"
            icon={<ExportOutlined />}
            onClick={() => handlePrintImage(record)}
            style={{ marginRight: 8 }}
          >
            Export Image
          </Button>
        </span>
      ),
    },
  ];

  const handleExport = () => {
    toPng(invoiceRef.current)
      .then((dataUrl) => {
        const link = document.createElement("a");
        link.href = dataUrl;
        link.download = "invoice.png";
        link.click();
      })
      .catch((error) => {
        console.error("Export failed:", error);
      });
  };
  const handlePrintImage = (record) => {
    setInvoiceData(record);
    handleExport();
  };
  return (
    <div className="duration-200 bg-white dark:bg-gray-900 dark:text-white">
      <Navbar />
      <div className="duration-200 bg-white dark:bg-gray-900 dark:text-white border-brand-800">
        <div class="absolute opacity-0">
          <div
            ref={invoiceRef}
            class="p-10 max-w-md mx-auto border border-black shadow-md rounded"
          >
            <div class="flex justify-center font-bold text-2xl">Invoice</div>
            <div class="text-sm">Invoice ID: {invoiceData.invoiceId}</div>
            <div class="text-sm">Order ID: {invoiceData.orderId}</div>
            <div class="text-sm">Promotion ID: {invoiceData.promotionId}</div>
            <div class="text-sm">
              Promotion Name: {invoiceData.promotionName}
            </div>
            <div class="text-sm">Total Price: ${invoiceData.totalPrice}</div>
            <div class="text-sm">Staff ID: {invoiceData.staffId}</div>
            <div class="mt-5">
              <div class="flex justify-center font-bold text-lg">Bill to:</div>
              <div class="text-sm">
                Name: {invoiceData.order?.customer?.firstName}{" "}
                {invoiceData.order?.customer?.lastName}
              </div>
              <div class="text-sm">
                Address: {invoiceData.order?.customer?.address}
              </div>
              <div class="text-sm">
                Phone: {invoiceData.order?.customer?.phoneNumber}
              </div>
              <div class="text-sm">
                Email: {invoiceData.order?.customer?.email}
              </div>
            </div>
            <div class="mt-5">
              <div class="text-sm">
                Total: ${invoiceData.order?.totalAmount}
              </div>
            </div>
          </div>
        </div>
      </div>
      <div className="min-h-[550px] flex justify-center items-center py-12 sm:py-0">
        <div className="container">
          <Card extra="w-full sm:overflow-auto p-4">
            <header className="relative flex items-center justify-between">
              <div className="text-xl font-bold text-navy-700 dark:text-white">
                Invoice Display
              </div>
              <div className="flex items-center">
                <input
                  type="text"
                  value={searchId}
                  onChange={(e) => setSearchId(e.target.value)}
                  placeholder="Search by Order ID"
                  className="px-3 py-2 border rounded-lg me-2"
                />
                <button
                  type="button"
                  className="rounded-lg bg-gradient-to-br from-purple-600 to-blue-500 px-3 py-2.5 text-center text-sm font-medium text-white me-2 hover:bg-gradient-to-bl focus:outline-none focus:ring-4 focus:ring-blue-300 dark:focus:ring-blue-800"
                  onClick={handleSearch}
                >
                  Search
                </button>
              </div>
            </header>
            <Table
              dataSource={Customer}
              columns={columns}
              loading={loading}
              rowKey={(record) => record.displayId}
            />

            {/* Add/Edit Gold Price Modal */}
          </Card>
        </div>
      </div>
      <Footer />
    </div>
  );
};

const CustomerForm = ({ onFinish, initialValues }) => {
  const [form] = Form.useForm();

  useEffect(() => {
    form.resetFields();
    if (initialValues) {
      form.setFieldsValue(initialValues);
    }
  }, [initialValues, form]);

  const handleSubmit = async () => {
    try {
      const values = await form.validateFields();
      values.password = "abc123abc";
      onFinish(values);
      form.resetFields();
    } catch (error) {
      console.error("Validation failed:", error);
    }
  };

  return (
    <Form form={form} onFinish={handleSubmit}>
      <Form.Item
        name="firstName"
        labelCol={{ style: { width: "100px", display: "flex" } }}
        label="First Name"
        rules={[{ required: true, message: "Please enter FirstName" }]}
      >
        <Input placeholder="Enter FirstName" />
      </Form.Item>
      <Form.Item
        name="lastName"
        labelCol={{ style: { width: "100px", display: "flex" } }}
        label="Last Name"
        rules={[{ required: true, message: "Please enter LastName" }]}
      >
        <Input placeholder="Enter FirstName" />
      </Form.Item>
      <Form.Item
        name="email"
        labelCol={{ style: { width: "100px", display: "flex" } }}
        label="Email"
        rules={[
          { required: true, message: "Please enter Email" },
          { max: 100, message: "Email must be at most 20 characters" },
        ]}
      >
        <Input placeholder="Enter Email" />
      </Form.Item>
      <Form.Item
        name="phoneNumber"
        labelCol={{ style: { width: "100px", display: "flex" } }}
        label="PhoneNumber"
        rules={[
          {
            min: 10,
            max: 10,
            message: "PhoneNumber must be exactly 20 characters",
          },
        ]}
      >
        <Input placeholder="Enter PhoneNumber" />
      </Form.Item>
      <Form.Item
        name="address"
        labelCol={{ style: { width: "100px", display: "flex" } }}
        label="Address"
      >
        <Input placeholder="Enter Address" />
      </Form.Item>
      <Form.Item>
        <Button type="primary" htmlType="submit">
          {initialValues ? "Update" : "Add"}
        </Button>
      </Form.Item>
    </Form>
  );
};

export default Invoice;
