import React, { useState, useEffect } from "react";
import axios from "axios";
import { Row, Col, Table, Modal, Form, Input, Button, Popconfirm, message, DatePicker, InputNumber, Checkbox } from "antd";
import { EditOutlined, DeleteOutlined } from '@ant-design/icons';
import Card from "../../../components/card";
import { commonAPI } from "../../../api/common.api";
import { toast } from "react-toastify";
import moment from 'moment';
const PromotionTable = () => {
  const [Promotions, setPromotions] = useState([]);
  const [loading, setLoading] = useState(true);
  const [searchId, setSearchId] = useState("");
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [editData, setEditData] = useState(null);

  useEffect(() => {
    fetchPromotions();
  }, []);

  const fetchPromotions = async () => {
    try {
      const response = await commonAPI.getAPI("Promotions/GetPromotions");
      setPromotions(response.data);
      setLoading(false);
    } catch (error) {
      console.error("Error fetching promotions:", error);
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
        fetchPromotions();
      } else {
        const response = await commonAPI.getAPI(`Promotions/search/${searchId}`);
        setPromotions([response.data]);
        setLoading(false);
      }
    } catch (error) {
      console.error("Error searching promotion by ID:", error);
      setLoading(false);
    }
  };


  const handleAddPromotion = async (values) => {
    try {
      const token = localStorage.getItem("token");
      if (!token) {
        throw new Error("No token found. Please log in.");
      }
      await commonAPI.postAPI("Promotions/add", values);
      setIsModalOpen(false);
      fetchPromotions();
      message.success("Promotion added successfully");
    } catch (error) {
      console.error("Error adding Promotion:", error);
      message.error("Failed to add Promotion");
    }
  };


  const handleEditPromotion = async (values) => {
    try {
      
      const token = localStorage.getItem("token");
      if (!token) {
        throw new Error("No token found. Please log in.");
      }
      editData.name = values.name;
      editData.startDate = values.startDate;
      editData.endDate = values.endDate;
      editData.discount = values.discount;
      editData.points = values.points;
      editData.approved = values.approved;
      
      // backend đang return no content 
      await commonAPI.putAPI(`Promotions/update/${editData.promotionId}`, editData);
      setIsModalOpen(false);
      fetchPromotions();
      message.success("Promotion updated successfully");
    } catch (error) {
      console.error("Error updating Promotion:", error);
      message.error("Failed to update Promotion");
    }
  };


  const handleDeletePromotion = async (displayId) => {
    try {
      const token = localStorage.getItem("token");
      if (!token) {
        throw new Error("No token found. Please log in.");
      }

      await commonAPI.deleteAPI(`Promotions/DeletePromotion/${displayId}`);
      fetchPromotions();
      message.success("Promotion deleted successfully");
    } catch (error) {
      console.error("Error deleting Promotion:", error);
      message.error("Failed to delete Promotion");
    }
  };

  const handleEditModalOpen = (record) => {
    if (!record) return;
    
    var cloneRecord = JSON.parse(JSON.stringify(record));
    cloneRecord.startDate = moment(cloneRecord.startDate, 'YYYY-MM-DD');
    cloneRecord.endDate = moment(cloneRecord.endDate, 'YYYY-MM-DD');
    setEditData(cloneRecord);
    setIsModalOpen(true);
  };

  const handleClickBtnAdd = () => {
    setEditData(null);
    setIsModalOpen(true);
  }

  const columns = [
    {
      title: "PromotionID",
      dataIndex: "promotionId",
      key: "promotionId",
      align: "center",
    },
    {
      title: "Name",
      dataIndex: "name",
      key: "name",
      align: "center",
    },
    {
      title: "Start Date",
      dataIndex: "startDate",
      key: "startDate",
      align: "center",
      render: (text, record) => (
        <span style={{ display: "flex", justifyContent: "center" }}>
          { moment(record.startDate, 'YYYY-MM-DD').format('DD/MM/YYYY')}
        </span>
      )
    },
    {
      title: "End Date",
      dataIndex: "endDate",
      key: "endDate",
      align: "center",
      render: (text, record) => (
        <span style={{ display: "flex", justifyContent: "center" }}>
          { moment(record.endDate, 'YYYY-MM-DD').format('DD/MM/YYYY')}
        </span>
      )
    },
    {
      title: "Discount",
      dataIndex: "discount",
      key: "discount",
      align: "center",
    },
    {
      title: "Approved",
      dataIndex: "approved",
      key: "approved",
      align: "center",
      render: (text, record) => (
        <span style={{ display: "flex", justifyContent: "center" }}>
          {record.approved ? "True" : "False"}
        </span>
      ),
    },
    {
      title: "Approved By",
      dataIndex: "approvedBy",
      key: "approvedBy",
      align: "center"
    },
    {
      title: "Points",
      dataIndex: "points",
      key: "points",
      align: "center"
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
            icon={<EditOutlined />}
            onClick={() => handleEditModalOpen(record)}
            style={{ marginRight: 8 }}
          >
            Edit
          </Button>
          <Popconfirm
            title="Are you sure delete this promotion?"
            onConfirm={() => handleDeletePromotion(record.promotionId)}
            okText="Yes"
            cancelText="No"
          >
            <Button type="link" icon={<DeleteOutlined />} danger>
              Delete
            </Button>
          </Popconfirm>
        </span>
      ),
    },
  ];

  return (
    <Card extra="w-full sm:overflow-auto p-4">
      <header className="relative flex items-center justify-between">
        <div className="text-xl font-bold text-navy-700 dark:text-white">
          Promotion Display
        </div>
        <div className="flex items-center">
          <input
            type="text"
            value={searchId}
            onChange={(e) => setSearchId(e.target.value)}
            placeholder="Search by ID"
            className="px-3 py-2 border rounded-lg me-2"
          />
          <button
            type="button"
            className="rounded-lg bg-gradient-to-br from-purple-600 to-blue-500 px-3 py-2.5 text-center text-sm font-medium text-white me-2 hover:bg-gradient-to-bl focus:outline-none focus:ring-4 focus:ring-blue-300 dark:focus:ring-blue-800"
            onClick={handleSearch}
          >
            Search
          </button>
          <button
            type="button"
            className="rounded-lg bg-gradient-to-br from-purple-600 to-blue-500 px-3 py-2.5 text-center text-sm font-medium text-white me-2 hover:bg-gradient-to-bl focus:outline-none focus:ring-4 focus:ring-blue-300 dark:focus:ring-blue-800"
            onClick={handleClickBtnAdd}
          >
            Add Promotion
          </button>
        </div>
      </header>
      <Table
        dataSource={Promotions}
        columns={columns}
        loading={loading}
        rowKey={(record) => record.displayId}
      />

      {/* Add/Edit Gold Price Modal */}
      <Modal
        title={editData ? "Edit Promotion" : "Add Promotion"}
        visible={isModalOpen}
        onCancel={() => setIsModalOpen(false)}
        footer={null}
      >
        <PromotionForm
          onFinish={editData ? handleEditPromotion : handleAddPromotion}
          initialValues={editData}
        />
      </Modal>
    </Card>
  );
};

const PromotionForm = ({ onFinish, initialValues }) => {
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
      // validate 
      values.startDate =  values.startDate.format('YYYY-MM-DD');
      values.endDate = values.endDate.format('YYYY-MM-DD');
      if (values.startDate > values.endDate){
        toast.error("The end date cannot be greater than the start date");
        return;
      }
      if (values.startDate > values.endDate){
        toast.error("The end date cannot be greater than the start date");
        return;
      }
      if (values.discount < 0){
        toast.error("The discount cannot be negative");
        return;
      }
      if (values.approved == null){
        values.approved = false;
      }
      onFinish(values);
      form.resetFields();
    } catch (error) {
      console.error("Validation failed:", error);
    }
  };

  return (
    <Form form={form} onFinish={handleSubmit}>
      <Form.Item
        name="name"
        labelCol={{ style: { width: '80px', display: 'flex' } }}
        label="Name"
        rules={[{ required: true, message: "Please enter Name" }]}
      >
        <Input placeholder="Enter Name" />
      </Form.Item>
      <Row gutter={16}>
        <Col span={12}>
          <Form.Item
            name="startDate"
            label="Start Date"
            labelCol={{ style: { width: '80px', display: 'flex' } }}
            rules={[{ required: true, message: 'Please select start date' }]}
          >
            <DatePicker  />
          </Form.Item>
        </Col>
        <Col span={12}>
          <Form.Item
            name="endDate"
            label="End Date"
            labelCol={{ style: { width: '80px', display: 'flex' } }}
            rules={[{ required: true, message: 'Please select end date' }]}
          >
            <DatePicker  />
          </Form.Item>
        </Col>
      </Row>

      <Form.Item
        name="discount"
        label="Discount"
        labelCol={{ style: { width: '80px', display: 'flex' } }}
        rules={[{ required: true, message: 'Please enter discount' }]}
      >
        <InputNumber placeholder="Enter Discount" step={0.01} />
      </Form.Item>


      <Form.Item
        name="points"
        label="Points"
        labelCol={{ style: { width: '80px', display: 'flex' } }}
        rules={[{ required: true, message: 'Please enter Points' }]}
      >
        <InputNumber placeholder="Enter Points" />
      </Form.Item>
      <Form.Item
        name="approved"
        valuePropName="checked"
        label="Approved"
        labelCol={{ style: { width: '80px', display: 'flex' } }}
      >
        <Checkbox></Checkbox>
      </Form.Item>
      <Form.Item>
        <Button type="primary" htmlType="submit">
          {initialValues ? "Update" : "Add"}
        </Button>
      </Form.Item>
    </Form>
  );
};

export default PromotionTable;


