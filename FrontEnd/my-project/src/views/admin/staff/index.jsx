import React, { useState, useEffect } from "react";
import axios from "axios";
import { Table, Modal, Form, Input, Button, Popconfirm, message } from "antd";
import { EditOutlined, DeleteOutlined } from '@ant-design/icons';
import Card from "../../../components/card";
import { commonAPI } from "../../../api/common.api";
import moment from 'moment';
const StaffTable = () => {
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


  const columns = [
    {
      title: "StaffId",
      dataIndex: "staffId",
      key: "staffId",
    },
    {
      title: "StaffName",
      dataIndex: "staffName",
      key: "staffName",
    },
    {
      title: "Email",
      dataIndex: "email",
      key: "email",
    },
    {
      title: "StoreID",
      dataIndex: "storeId",
      key: "storeId",
    },
    {
      title: "Phone",
      dataIndex: "phone",
      key: "phone",
    },
    {
      title: "Hire Date",
      dataIndex: "hireDate",
      key: "hireDate",
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

        </div>
      </header>
      <Table
        dataSource={ReturnPolicies}
        columns={columns}
        loading={loading}
        rowKey={(record) => record.displayId}
      />


    </Card>
  );
};


export default StaffTable;
