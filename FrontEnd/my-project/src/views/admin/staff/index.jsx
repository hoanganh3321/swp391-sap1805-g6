import React, { useState, useEffect } from "react";
import { Table, Modal, Form, Input, Button, Popconfirm, message, DatePicker, Row, Col } from "antd";
import Card from "../../../components/card";
import { commonAPI } from "../../../api/common.api";
import moment from 'moment';

const StaffTable = () => {
  const [ReturnPolicies, setReturnPolicies] = useState([]);
  const [loading, setLoading] = useState(true);
  const [searchId, setSearchId] = useState("");
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [editData, setEditData] = useState(null);
  const [form] = Form.useForm();

  useEffect(() => {
    fetchReturnPolicies();
  }, []);

  const fetchReturnPolicies = async () => {
    try {
      const response = await commonAPI.getAPI("Staff/list");
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
        const response = await commonAPI.getAPI(`Staff/search?email=${searchId}`); 
        setReturnPolicies([response.data]);
        setLoading(false);
      }
    } catch (error) {
      console.error("Error searching staff by ID:", error);
      setLoading(false);
      setReturnPolicies([]);
    }
  };

  const handleAddStaff = async (values) => {
    try {
      const token = localStorage.getItem("token");
      if (!token) {
        throw new Error("No token found. Please log in.");
      }

      const formattedValues = {
        ...values,
        hireDate: values.hireDate.format('YYYY-MM-DD'),
      };

      const response = await commonAPI.postAPI("Staff/add", formattedValues, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      message.success("Staff added successfully!");
      setIsModalOpen(false);
      fetchReturnPolicies();
    } catch (error) {
      console.error("Error adding staff:", error);
      message.error("Failed to add staff.");
    }
  };

  const columns = [
    {
      title: "StaffId",
      dataIndex: "staffId",
      key: "staffId",
      align: "center"
    },
    {
      title: "StaffName",
      dataIndex: "staffName",
      key: "staffName",
      align: "center"
    },
    {
      title: "Email",
      dataIndex: "email",
      key: "email",
      align: "center"
    },
    {
      title: "StoreID",
      dataIndex: "storeId",
      key: "storeId",
      align: "center"
    },
    {
      title: "Phone",
      dataIndex: "phone",
      key: "phone",
      align: "center"
    },
    {
      title: "Hire Date",
      dataIndex: "hireDate",
      key: "hireDate",
      align: "center",
      render: (text, record) => (
        <span style={{ display: "flex", justifyContent: "center" }}>
          { moment(record.hireDate, 'YYYY-MM-DD').format('DD/MM/YYYY')}
        </span>
      ),
    },
  ];

  return (
    <Card extra="w-full sm:overflow-auto p-4">
      <header className="relative flex items-center justify-between">
        <div className="text-xl font-bold text-navy-700 dark:text-white">
          Staff Display
        </div>
        <div className="flex items-center">
          <input
            type="text"
            value={searchId}
            onChange={(e) => setSearchId(e.target.value)}
            placeholder="Search by email"
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
            onClick={() => setIsModalOpen(true)}
          >
            Add Staff
          </button>
        </div>
      </header>
      <Table
        dataSource={ReturnPolicies}
        columns={columns}
        loading={loading}
        rowKey={(record) => record.displayId}
      />
      <Modal
        title="Add Staff"
        visible={isModalOpen}
        onCancel={() => setIsModalOpen(false)}
        footer={null}
      >
        <Form form={form} onFinish={handleAddStaff} layout="vertical">
          <Row gutter={16}>
            <Col span={12}>
              <Form.Item
                label="Staff Name"
                name="staffName"
                rules={[{ required: true, message: "Please input the staff name!" }]}
              >
                <Input />
              </Form.Item>
            </Col>
            <Col span={12}>
              <Form.Item
                label="Email"
                name="email"
                rules={[{ required: true, message: "Please input the email!" }]}
              >
                <Input />
              </Form.Item>
            </Col>
          </Row>
          <Row gutter={16}>
            <Col span={12}>
              <Form.Item
                label="Password"
                name="password"
                rules={[{ required: true, message: "Please input the password!" }]}
              >
                <Input.Password />
              </Form.Item>
            </Col>
            <Col span={12}>
              <Form.Item
                label="Role ID"
                name="roleId"
                rules={[{ required: true, message: "Please input the role ID!" }]}
              >
                <Input />
              </Form.Item>
            </Col>
          </Row>
          <Row gutter={16}>
            <Col span={12}>
              <Form.Item
                label="Store ID"
                name="storeId"
                rules={[{ required: true, message: "Please input the store ID!" }]}
              >
                <Input />
              </Form.Item>
            </Col>
            <Col span={12}>
              <Form.Item
                label="Phone"
                name="phone"
                rules={[{ required: true, message: "Please input the phone number!" }]}
              >
                <Input />
              </Form.Item>
            </Col>
          </Row>
          <Row gutter={16}>
            <Col span={24}>
              <Form.Item
                label="Hire Date"
                name="hireDate"
                rules={[{ required: true, message: "Please select the hire date!" }]}
              >
                <DatePicker format="YYYY-MM-DD" />
              </Form.Item>
            </Col>
          </Row>
          <Form.Item>
            <Button type="primary" htmlType="submit" style={{ width: '100%' }}>
              Add Staff
            </Button>
          </Form.Item>
        </Form>
      </Modal>
    </Card>
  );
};

export default StaffTable;
