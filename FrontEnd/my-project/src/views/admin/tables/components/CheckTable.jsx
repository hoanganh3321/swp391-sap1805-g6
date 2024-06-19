import React, { useState, useEffect } from "react";
import Card from "../../../../components/card";
import ButtonCreate from "../../../../components/atom/ButtonCreate/ButtonCreate";
import { Table, Form, Input, InputNumber, Button, Modal, Checkbox, Row, Col } from 'antd';
import { EditOutlined, DeleteOutlined } from '@ant-design/icons';

const CheckTable = (props) => {
  const { tableData } = props;
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
    form.setFieldsValue({
      ...record,
      isBuyback: !!record.isBuyback, // Ensure isBuyback is boolean for Checkbox
    });
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

  const columns = [
    {
      title: 'Product Name',
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
        <div style={{ display: 'flex' }}>
          <Button
            type="primary"
            icon={<EditOutlined />}
            onClick={() => handleEdit(record)}
            style={{ marginRight: 8 }}
          >
            Edit
          </Button>
          <Button
            type="danger"
            icon={<DeleteOutlined />}
            onClick={() => handleDelete(record.id)}
          >
            Delete
          </Button>
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

      <div className="table-container">
        <Table columns={columns} dataSource={data} pagination={false} />
      </div>

      <Modal
        title="Edit Product"
        visible={isEditModalOpen}
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
        width={800} // Set width of the modal
      >
        <Form
          form={form}
          onFinish={handleUpdate}
          initialValues={editingProduct} // Set initial values for all fields
          labelCol={{ span: 8 }}
          wrapperCol={{ span: 16 }}
        >
          <Form.Item
            label="Product Name"
            name="productName"
            rules={[{ required: true, message: 'Please input the product name!' }]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            label="Price"
            name="price"
            rules={[{ required: true, message: 'Please input the price!' }]}
          >
            <InputNumber min={0} />
          </Form.Item>
          <Form.Item
            label="Weight"
            name="weight"
            rules={[{ required: true, message: 'Please input the weight!' }]}
          >
            <InputNumber min={0} />
          </Form.Item>
          <Form.Item
            label="Quantity"
            name="quantity"
            rules={[{ required: true, message: 'Please input the quantity!' }]}
          >
            <InputNumber min={0} />
          </Form.Item>
          <Form.Item
            label="Warranty"
            name="warranty"
            rules={[{ required: true, message: 'Please input the warranty!' }]}
          >
            <Input/>
          </Form.Item>
          <Form.Item
            label="Image Link"
            name="image"
          >
            <Input />
          </Form.Item>
          <Form.Item
            label="Barcode"
            name="barcode"
          >
            <Input />
          </Form.Item>
          <Form.Item
            label="Manufacturing Cost"
            name="manufacturingCost"
          >
            <InputNumber min={0} />
          </Form.Item>
          <Form.Item
            label="Stone Cost"
            name="stoneCost"
          >
            <InputNumber min={0} />
          </Form.Item>
          <Form.Item
            label="Is Buyback"
            name="isBuyback"
            valuePropName="checked"
          >
            <Checkbox />
          </Form.Item>
          <Form.Item
            label="Category ID"
            name="categoryId"
          >
            <InputNumber min={0} />
          </Form.Item>
          <Form.Item
            label="Store ID"
            name="storeId"
          >
            <InputNumber min={0} />
          </Form.Item>
        </Form>
      </Modal>
    </Card>
  );
};

export default CheckTable;
