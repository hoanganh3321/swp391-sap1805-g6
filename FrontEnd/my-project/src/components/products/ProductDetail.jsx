import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import Navbar from "../navbar/Navbar";
import Footer from "../footer/FooterHomePage";
import { useNavigate } from "react-router-dom";
import { Select, Button } from 'antd';
import { commonAPI } from "../../api/common.api";
import { toast } from "react-toastify";
const { Option } = Select;
function ProductDetail() {
  const { id } = useParams();
  const [product, setProduct] = useState({});
  const [showAlert, setShowAlert] = useState(false);
  const [showError, setShowError] = useState(false);
  const [quantity, setQuantity] = useState(1);
  const navigate = useNavigate();
  const [selectedCustomer, setSelectedCustomer] = useState(null);
  const [customers, setCustomer] = useState([]);
  useEffect(() => {
    const fetchProduct = async () => {
      try {
        const response = await fetch(
          `https://localhost:7002/api/product/search/${id}`
        );
        if (!response.ok) {
          throw new Error("Network response was not ok");
        }
        const data = await response.json();
        setProduct(data);
      } catch (error) {
        console.error("Failed to fetch product:", error);
      }
    };
    fetchProduct();
  }, [id]);

  useEffect(() => {
    fetchCustomer();
  }, []);

  const fetchCustomer = async () => {
    try {
      const response = await commonAPI.getAPI("Customer/getall");
      setCustomer(response.data);
    } catch (error) {
      console.error("Error fetching Customer:", error);
    }
  };


  const handleAddToCart = async () => {
    if (!selectedCustomer){
      toast.error("No Customer select");
      return;
    }
    try {
      const response = await commonAPI.postAPI("StaffOrder/addProductToCart", {
        customerId: selectedCustomer, // Replace with actual customerId
        productID: product.productId,
        quantity: quantity, // Use selected quantity
      });
      if (response.status == 200){
        navigate(`/cart/${selectedCustomer}`);
      }
      throw new Error("Failed to add product to cart");
    } catch (error) {
      console.error("Failed to add product to cart:", error);
      setShowError(true);
      setShowAlert(false);
      setTimeout(() => {
        setShowError(false);
      }, 3000);
    }
  };

  if (!Object.keys(product).length) return <div>Product Not Found</div>;

  return (
    <>
      <Navbar />
      <section className="overflow-hidden text-gray-600 body-font">
        <div className="container px-5 py-24 mx-auto">
          <div className="flex flex-wrap mx-auto lg:w-4/5">
            <img
              alt={product?.productName}
              className="lg:w-1/2 w-full lg:h-auto h-64 max-h-[400px] object-contain object-center rounded"
              src={product?.image}
            />
            <div className="w-full mt-6 lg:w-1/2 lg:pl-10 lg:py-6 lg:mt-0">
              <h1 className="mb-1 text-3xl font-medium text-gray-900 title-font">
                {product?.productName}
              </h1>
              <p className="leading-relaxed">{product?.warranty}</p>
              <div className="flex items-center pb-5 mt-6 mb-5 border-b-2 border-gray-100"></div>
              <div className="flex flex-col mb-4">
                <label htmlFor="customer" className="mb-2 font-medium">
                  Select Customer
                </label>
                <Select
                  id="customer"
                  showSearch
                  placeholder="Search customers"
                  optionFilterProp="children"
                  value={selectedCustomer}
                  onChange={setSelectedCustomer}
                  filterOption={(input, option) =>
                    option.children.toLowerCase().includes(input.toLowerCase())
                  }
                  className="w-full"
                >
                  {customers.map((customer) => (
                    <Option key={customer.customerId} value={customer.customerId}>
                      {customer.lastName}
                    </Option>
                  ))}
                </Select>
              </div>
              <div className="flex items-center justify-between">
                <span className="text-2xl font-medium text-gray-900 title-font">
                  ${product?.price}
                </span>
                <div className="flex ml-4">
                  <input
                    type="number"
                    value={quantity}
                    onChange={(e) => setQuantity(Number(e.target.value))}
                    min="1"
                    className="w-16 p-2 mr-4 border border-gray-300 rounded"
                  />
                  <button
                    className="flex px-6 py-2 border-0 rounded text-hemp bg-bloom focus:outline-none"
                    onClick={handleAddToCart}
                  >
                    Add to Cart
                  </button>
                </div>
              </div>
              {showAlert && (
                <div className="mt-4 text-green-600">Added to Cart</div>
              )}
              {showError && (
                <div className="mt-4 text-red-600">Failed to add to Cart</div>
              )}
            </div>
          </div>
        </div>
      </section>
      <Footer />
    </>
  );
}

export default ProductDetail;
