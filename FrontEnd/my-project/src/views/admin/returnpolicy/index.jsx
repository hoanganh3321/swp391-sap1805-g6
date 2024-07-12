import React, { useState, useEffect } from "react";
import axios from "axios";
import { Table, Modal, Form, Input, Button, Popconfirm, message } from "antd";
import { EditOutlined, DeleteOutlined } from '@ant-design/icons';
import Card from "../../../components/card";
import { commonAPI } from "../../../api/common.api";

const ReturnPolicyTable = () => {
  const [ReturnPolicies, setReturnPolicies] = useState([]);
  const [loading, setLoading] = useState(true);
  const [searchId, setSearchId] = useState("");
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [editData, setEditData] = useState(null);

  useEffect(() => {
    fetchReturnPolicies();
  }, []);

  const fetchReturnPolicies = async () => {
    try {
      const response = await commonAPI.getAPI("ReturnPolicy/list");
      setReturnPolicies(response.data);
      setLoading(false);
    } catch (error) {
      console.error("Error fetching promotion:", error);
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
        fetchReturnPolicies();
      } else {
        const response = await commonAPI.getAPI(`ReturnPolicy/search/${searchId}`);
        setReturnPolicies([response.data]);
        setLoading(false);
      }
    } catch (error) {
      console.error("Error searching return policy by ID:", error);
      setLoading(false);
    }
  };


  const handleAddReturnPolicy = async (values) => {
    try {
      const token = localStorage.getItem("token");
      if (!token) {
        throw new Error("No token found. Please log in.");
      }
      await commonAPI.postAPI("ReturnPolicy/add", values);
      setIsModalOpen(false);
      fetchReturnPolicies();
      message.success("Return policy added successfully");
    } catch (error) {
      console.error("Error adding return policy:", error);
      message.error("Failed to add return policy");
    }
  };


  const handleEditReturnPolicy = async (values) => {
    try {
      const token = localStorage.getItem("token");
      if (!token) {
        throw new Error("No token found. Please log in.");
      }
      editData.description = values.description;
      const response = await commonAPI.putAPI(`ReturnPolicy/update/${editData.policyId}`, editData);
      if (response.status === 200) {
        setIsModalOpen(false);
        fetchReturnPolicies();
        message.success("Return policy updated successfully");
      } else {
        throw new Error("Failed to update Return policy");
      }
    } catch (error) {
      console.error("Error updating Return policy:", error);
      message.error("Failed to update Return policy");
    }
  };


  const handleDeleteReturnPolicy = async (displayId) => {
    try {
      const token = localStorage.getItem("token");
      if (!token) {
        throw new Error("No token found. Please log in.");
      }

      await commonAPI.deleteAPI(`ReturnPolicy/delete/${displayId}`);
      fetchReturnPolicies();
      message.success("Return policy deleted successfully");
    } catch (error) {
      console.error("Error deleting Return policy:", error);
      message.error("Failed to delete Return policy");
    }
  };

  const handleClickBtnAdd = () => {
    setEditData(null);
    setIsModalOpen(true);
  }

  const handleEditModalOpen = (record) => {
    if (!record)  return;
    var cloneRecord = JSON.parse(JSON.stringify(record));
    setEditData(record);
    setIsModalOpen(true);
  };

  const columns = [
    {
      title: "PolicyID",
      dataIndex: "policyId",
      key: "policyId",
      align: "center"
    },
    {
      title: "Description",
      dataIndex: "description",
      key: "description",
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
            title="Are you sure delete this return policy?"
            onConfirm={() => handleDeleteReturnPolicy(record.policyId)}
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
          Return Policy Display
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
            Add Return Policy
          </button>
        </div>
      </header>
      <Table
        dataSource={ReturnPolicies}
        columns={columns}
        loading={loading}
        rowKey={(record) => record.displayId}
      />

      {/* Add/Edit Gold Price Modal */}
      <Modal
        title={editData ? "Edit Return Policy" : "Add Return Policy"}
        visible={isModalOpen}
        onCancel={() => setIsModalOpen(false)}
        footer={null}
      >
        <ReturnPolicyForm
          onFinish={editData ? handleEditReturnPolicy : handleAddReturnPolicy}
          initialValues={editData}
        />
      </Modal>
    </Card>
  );
};

const ReturnPolicyForm = ({ onFinish, initialValues }) => {
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
      <Form.Item
        name="description"
        label="Description"
        rules={[{ required: true, message: "Please enter Description" }]}
      >
        <Input placeholder="Enter Description" />
      </Form.Item>
      <Form.Item>
        <Button type="primary" htmlType="submit">
          {initialValues ? "Update" : "Add"}
        </Button>
      </Form.Item>
    </Form>
  );
};

export default ReturnPolicyTable;
