import React from "react";
import { Modal, Form, Input, InputNumber, Checkbox, Button, Row, Col } from "antd";

const ButtonCreate = ({ isOpen, onClose }) => {
  const [form] = Form.useForm();

  const handleSubmit = async (values) => {
    const productData = {
      productName: values.productName,
      price: values.price,
      barcode: values.barcode,
      productPrice: values.productPrice,
      weight: values.weight,
      stoneCost: values.stoneCost,
      warranty: values.warranty,
      quantity: values.quantity,
      manufacturingCost: values.manufacturingCost,
      isBuyback: values.isBuyback,
      categoryId: values.categoryId,
      storeId: values.storeId,
      image: values.image,
    };

    try {
      const response = await fetch("https://localhost:7002/api/product/add", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          // "Authorization": `Bearer ${token}`,
        },
        body: JSON.stringify(productData),
      });
  
      if (response.ok) {
        console.log("Product added successfully");
        form.resetFields(); 
        onClose();
      } else {
        console.error("Error adding product:", response.status, response.statusText);
      }
    } catch (error) {
      console.error("Error:", error);
    }
  };

  return (
    <Modal
      title="Add Product"
      visible={isOpen}
      onCancel={onClose}
      footer={null}
    >
      <Form
        form={form}
        layout="vertical"
        onFinish={handleSubmit}
      >
        <Row gutter={16}>
          <Col span={12}>
            <Form.Item
              name="productName"
              label="Product Name"
              rules={[{ required: true, message: 'Please input the product name!' }]}
            >
              <Input />
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item
              name="weight"
              label="Weight"
              rules={[{ required: true, message: 'Please input the weight!' }]}
            >
              <InputNumber min={0} className="w-full" />
            </Form.Item>
          </Col>
        </Row>
        <Row gutter={16}>
          <Col span={12}>
            <Form.Item
              name="price"
              label="Price"
              rules={[{ required: true, message: 'Please input the product price!' }]}
            >
              <InputNumber min={0} className="w-full" />
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item
              name="stoneCost"
              label="Stone Cost"
              rules={[{ required: true, message: 'Please input the stone cost!' }]}
            >
              <InputNumber min={0} className="w-full" />
            </Form.Item>
          </Col>
        </Row>
        <Row gutter={16}>
          <Col span={12}>
            <Form.Item
              name="warranty"
              label="Warranty"
              rules={[{ required: true, message: 'Please input the warranty!' }]}
            >
              <Input />
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item
              name="quantity"
              label="Quantity"
              rules={[{ required: true, message: 'Please input the quantity!' }]}
            >
              <InputNumber min={0} className="w-full" />
            </Form.Item>
          </Col>
        </Row>
        <Row gutter={16}>
          <Col span={12}>
            <Form.Item
              name="manufacturingCost"
              label="Manufacturing Cost"
              rules={[{ required: true, message: 'Please input the manufacturing cost!' }]}
            >
              <InputNumber min={0} className="w-full" />
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item
              name="isBuyback"
              valuePropName="checked"
            >
              <Checkbox>Is Buyback</Checkbox>
            </Form.Item>
          </Col>
        </Row>
        <Row gutter={16}>
          <Col span={12}>
            <Form.Item
              name="categoryId"
              label="Category ID"
              rules={[{ required: true, message: 'Please input the category ID!' }]}
            >
              <InputNumber min={0} className="w-full" />
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item
              name="storeId"
              label="Store ID"
              rules={[{ required: true, message: 'Please input the store ID!' }]}
            >
              <InputNumber min={0} className="w-full" />
            </Form.Item>
          </Col>
        </Row>
        <Row gutter={16}>
          <Col span={12}>
            <Form.Item
              name="barcode"
              label="Barcode"
              rules={[{ required: true, message: 'Please input the barcode!' }]}
            >
              <Input />
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item
              name="image"
              label="Image URL"
              rules={[{ required: true, message: 'Please input the image URL!' }]}
            >
              <Input />
            </Form.Item>
          </Col>
        </Row>
        <Form.Item className="text-right">
          <Button onClick={onClose} style={{ marginRight: 8 }}>
            Cancel
          </Button>
          <Button type="primary" htmlType="submit">
            Add
          </Button>
        </Form.Item>
      </Form>
    </Modal>
  );
};

export default ButtonCreate;
