import React, { useState, useEffect } from "react";
import axios from "axios";
import { Table, Modal, Form, Input, Button, Popconfirm, message } from "antd";
import { EditOutlined, DeleteOutlined } from '@ant-design/icons';
import Card from "../../../components/card";
import { commonAPI } from "../../../api/common.api";

const GoldPriceTable = () => {
  const [goldPrices, setGoldPrices] = useState([]);
  const [loading, setLoading] = useState(true);
  const [searchId, setSearchId] = useState("");
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [editData, setEditData] = useState(null);

  useEffect(() => {
    fetchGoldPrices();
  }, []);

  const fetchGoldPrices = async () => {
    try {
      const response = await axios.get(
        "https://localhost:7002/api/GoldPriceDisplay/getAllGoldPrice"
      );
      setGoldPrices(response.data);
      setLoading(false);
    } catch (error) {
      console.error("Error fetching gold prices:", error);
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
        fetchGoldPrices();
      } else {
        const response = await axios.get(
          `https://localhost:7002/api/GoldPriceDisplay/GetGoldPriceById/${searchId}`,
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );
        setGoldPrices([response.data]);
        setLoading(false);
      }
    } catch (error) {
      console.error("Error searching gold prices by ID:", error);
      setLoading(false);
    }
  };
  

const handleAddGoldPrice = async (values) => {
  try {
    const token = localStorage.getItem("token");
    if (!token) {
      throw new Error("No token found. Please log in.");
    }

    await axios.post(
      "https://localhost:7002/api/GoldPriceDisplay/AddGoldPrice",
      values,
      {
        headers: {
          Authorization: `Bearer ${token}`, // Set the Authorization header
        },
      }
    );

    setIsModalOpen(false);
    fetchGoldPrices();
    message.success("Gold price added successfully");
  } catch (error) {
    console.error("Error adding gold price:", error);
    message.error("Failed to add gold price");
  }
};


const handleEditGoldPrice = async (values) => {
  try {

    const token = localStorage.getItem("token");
    if (!token) {
      throw new Error("No token found. Please log in.");
    }
    editData.deviceId = values.deviceId;
    editData.location = values.location;
    editData.goldPrice = values.goldPrice;
    const response = await commonAPI.putAPI(`GoldPriceDisplay/UpdateGoldPrice/${editData.displayId}`, editData);
    setIsModalOpen(false);
    fetchGoldPrices();
    message.success("Gold price updated successfully");

  } catch (error) {
    console.error("Error updating gold price:", error);
    message.error("Failed to update gold price");
  }
};


const handleDeleteGoldPrice = async (displayId) => {
  try {
    const token = localStorage.getItem("token");
    if (!token) {
      throw new Error("No token found. Please log in.");
    }
    await axios.delete(
      `https://localhost:7002/api/GoldPriceDisplay/DeleteGoldPrice/${displayId}`,
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    );
    fetchGoldPrices();
    message.success("Gold price deleted successfully");
  } catch (error) {
    console.error("Error deleting gold price:", error);
    message.error("Failed to delete gold price");
  }
};

  const handleEditModalOpen = (record) => {
    if (!record) return;
    var cloneRecord = JSON.parse(JSON.stringify(record));
    setEditData(cloneRecord);
    setIsModalOpen(true);
  };

  const handleClickBtnAdd = () => {
    setEditData(null);
    setIsModalOpen(true);
  }

  const columns = [
    {
      title: "DisplayId",
      dataIndex: "displayId",
      key: "displayId",
    },
    {
      title: "DeviceId",
      dataIndex: "deviceId",
      key: "deviceId",
    },
    {
      title: "Location",
      dataIndex: "location",
      key: "location",
    },
    {
      title: "GoldPrice",
      dataIndex: "goldPrice",
      key: "goldPrice",
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
          title="Are you sure delete this gold price?"
          onConfirm={() => handleDeleteGoldPrice(record.displayId)}
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
          Gold Price Display
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
            Add GoldPrice
          </button>
        </div>
      </header>
      <Table
        dataSource={goldPrices}
        columns={columns}
        loading={loading}
        rowKey={(record) => record.displayId} 
      />

      {/* Add/Edit Gold Price Modal */}
      <Modal
        title={editData ? "Edit Gold Price" : "Add Gold Price"}
        visible={isModalOpen}
        onCancel={() => setIsModalOpen(false)}
        footer={null}
      >
        <GoldPriceForm
          onFinish={editData ? handleEditGoldPrice : handleAddGoldPrice}
          initialValues={editData}
        />
      </Modal>
    </Card>
  );
};

const GoldPriceForm = ({ onFinish, initialValues }) => {
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
      onFinish(values); 
      form.resetFields(); 
    } catch (error) {
      console.error("Validation failed:", error);
    }
  };

  return (
    <Form form={form} onFinish={handleSubmit}>
      {initialValues && (
        <Form.Item
          name="displayId"
          label="Display ID"
          rules={[{ required: true, message: "Please enter Display ID" }]}
        >
          <Input placeholder="Enter Display ID" disabled />
        </Form.Item>
      )}
      <Form.Item
        name="deviceId"
        label="Device ID"
        rules={[{ required: true, message: "Please enter Device ID" }]}
      >
        <Input placeholder="Enter Device ID" />
      </Form.Item>
      <Form.Item
        name="location"
        label="Location"
        rules={[{ required: true, message: "Please enter Location" }]}
      >
        <Input placeholder="Enter Location" />
      </Form.Item>
      <Form.Item
        name="goldPrice"
        label="Gold Price"
        rules={[{ required: true, message: "Please enter Gold Price" }]}
      >
        <Input placeholder="Enter Gold Price" />
      </Form.Item>
      <Form.Item>
        <Button type="primary" htmlType="submit">
          {initialValues ? "Update" : "Add"}
        </Button>
      </Form.Item>
    </Form>
  );
};

export default GoldPriceTable;
