import React, { useMemo, useState, useEffect } from "react";
import Card from "../../../../components/card";
import ButtonCreate from "../../../../components/atom/ButtonCreate/ButtonCreate";
import ErrorModal from "./ErrorModal";
import { Space, Table, TagModal, Form, Input, Button, Modal } from 'antd';

const CheckTable = (props) => {
  const { columnsData, tableData } = props;
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isEditModalOpen, setIsEditModalOpen] = useState(false);
  const [searchId, setSearchId] = useState("");
  const [data, setData] = useState(tableData);
  const [errorMessage, setErrorMessage] = useState("");
  const [editingProduct, setEditingProduct] = useState(null);
  const [form] = Form.useForm();

  useEffect(() => {
    fetchData();
  }, []);

  const fetchData = async () => {
    try {
      const response = await fetch(`https://localhost:7002/api/product/list`);
      if (!response.ok) {
        throw new Error("Failed to fetch products");
      }
      const result = await response.json();
      setData(result);
    } catch (error) {
      setErrorMessage(error.message);
    }
  };

  const handleSearch = async () => {
    if (searchId.trim() === "") {
      setData(tableData); // Reset to original table data if search input is cleared
      setErrorMessage(""); // Clear any previous error messages
      return; // Exit the function if search ID is empty
    }

    try {
      const response = await fetch(
        `https://localhost:7002/api/product/search/${searchId}`
      );
      if (!response.ok) {
        throw new Error("Product not found");
      }
      const result = await response.json();
      setData([result]);
      setErrorMessage("");
    } catch (error) {
      setErrorMessage(error.message);
    }
  };

  // const handleDelete = async (id) => {
  //   console.log("Attempting to delete product with ProductId:", id);

  //   if (!window.confirm("Are you sure you want to delete this product?")) {
  //     return;
  //   }

  //   try {
  //     const response = await fetch(
  //       `https://localhost:7002/api/product/delete/${id}`,
  //       {
  //         method: 'DELETE',
  //         headers: {
  //           'Content-Type': 'application/json',
  //         }
  //       }
  //     );

  //     console.log("Fetch request completed with status:", response.status);

  //     if (!response.ok) {
  //       const errorData = await response.json();
  //       console.error('Error response:', errorData);
  //       throw new Error(errorData.title || 'Failed to delete product');
  //     }

  //     console.log("Product deleted successfully, updating state.");
  //     setData((prevData) => prevData.filter((product) => product.ProductId !== id));
  //     setErrorMessage('');
  //   } catch (error) {
  //     console.error('Delete error:', error);
  //     setErrorMessage(error.message);
  //   }
  // };
  const handleDelete = async (id) => {
    console.log("Attempting to delete product with ProductId:", id);

    if (!window.confirm("Are you sure you want to delete this product?")) {
      return;
    }

    try {
      const response = await fetch(
        `https://localhost:7002/api/product/delete/${id}`,
        {
          method: 'DELETE',
          headers: {
            'Content-Type': 'application/json',
          }
        }
      );

      console.log("Fetch request completed with status:", response.status);

      if (!response.ok) {
        const errorData = await response.json();
        console.error('Error response:', errorData);
        throw new Error(errorData.title || 'Failed to delete product');
      }

      console.log("Product deleted successfully, updating state.");
      fetchData();
      setErrorMessage('');
    } catch (error) {
      console.error('Delete error:', error);
      setErrorMessage(error.message);
    }
  };

  const handleEdit = (record) => {
    setEditingProduct(record);
    form.setFieldsValue(record);
    setIsEditModalOpen(true);
  };

  const handleUpdate = async (values) => {
    try {
      const response = await fetch(
        `https://localhost:7002/api/product/update/${editingProduct.id}`,
        {
          method: 'PUT',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(values)
        }
      );

      if (!response.ok) {
        const errorData = await response.json();
        throw new Error(errorData.title || 'Failed to update product');
      }

      setIsEditModalOpen(false);
      fetchData();
    } catch (error) {
      setErrorMessage(error.message);
    }
  };

  const column = [
    {
      title: 'ProductName',
      dataIndex: 'productName',
      key: 'productName',
    },
    {
      title: 'Price',
      dataIndex: 'price',
      key: 'price',
    },
    {
      title: 'Weight',
      dataIndex: 'weight',
      key: 'weight',
    },
    {
      title: 'Quantity',
      dataIndex: 'quantity',
      key: 'quantity',
    },
    {
      title: 'Action',
      key: 'action',
      render: (_, record) => (
        <div >
          <button onClick={() => handleEdit(record)}>Edit</button>
          <button onClick={()=>{handleDelete(record.id)}}>Delete</button>
        </div>
      ),
    },
  ];
  return (
    <Card extra={"w-full sm:overflow-auto p-4"}>
      <header className="relative flex items-center justify-between">
        <div className="text-xl font-bold text-navy-700 dark:text-white">
          Product
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
            onClick={() => setIsModalOpen(true)}
          >
            Add Product
          </button>
        </div>

        <ButtonCreate
          isOpen={isModalOpen}
          onClose={() => setIsModalOpen(false)}
        />
      </header>
      
      <Table columns={column} dataSource={data} />
      <Modal
        title="Edit Product"
        open={isEditModalOpen}
        onCancel={() => setIsEditModalOpen(false)}
        footer={[
          <Button key="back" onClick={() => setIsEditModalOpen(false)}>
            Cancel
          </Button>,
          <Button
            key="submit"
            type="primary"
            onClick={() => form.submit()}
          >
            Save
          </Button>,
        ]}
      >
        <Form
          form={form}
          layout="vertical"
          onFinish={handleUpdate}
        >
          <Form.Item
            name="productName"
            label="Product Name"
            rules={[{ required: true, message: 'Please input the product name!' }]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            name="price"
            label="Price"
            rules={[{ required: true, message: 'Please input the price!' }]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            name="weight"
            label="Weight"
            rules={[{ required: true, message: 'Please input the weight!' }]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            name="quantity"
            label="Quantity"
            rules={[{ required: true, message: 'Please input the quantity!' }]}
          >
            <Input />
          </Form.Item>
        </Form>
      </Modal>
    </Card>
  );
};

export default CheckTable;
