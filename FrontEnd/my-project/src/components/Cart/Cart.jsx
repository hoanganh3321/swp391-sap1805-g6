import React, { useEffect, useState } from 'react';
import { Link, json, useNavigate } from 'react-router-dom';
import { commonAPI } from "../../api/common.api";
import { useParams } from "react-router-dom";
import Navbar from "../navbar/Navbar";
import Footer from "../footer/FooterHomePage";

function Cart() {
  const navigate = useNavigate();
  const [total, setTotal] = useState(0);
  const [carts, setCarts] = useState([]);
  const [orderDetails, setOrderDetails] = useState([]);
  const { customerId } = useParams();
  useEffect(() => {
    const fetchCart = async () => {
      try {
        const response = await commonAPI.getAPI(`StaffOrder/viewCart/${customerId}`);
        if (response.status === 200) {
          setCarts(response.data); 
          if (response.data?.length > 0){
            setOrderDetails(response.data[0].orderDetails);  
          }
          
        }
        throw new Error('Network response was not ok');
      } catch (error) {
        console.error('Failed to fetch cart:', error);
      }
    };
    fetchCart();
  }, [customerId]);

  useEffect(() => {
    const totalPrice = carts.reduce((acc, item) => {
      return acc + (item.price * item.quantity);
    }, 0);
    setTotal(totalPrice);
  }, [carts]);

  const removeProduct = async (productId, quantity) => {
    try {
      const response = await  commonAPI.putAPI(`StaffOrder/DeleteFromCart`, 
      {  customerId: customerId, 
        productID: productId,
        quantity: quantity}
      );
      if (response.status === 200) {
        carts.forEach(x => {
          x.orderDetails = x.orderDetails.filter(item => item.productId !== productId);
        });
        // carts = JSON.parse(JSON.stringify(carts));
        //const updatedCart = carts.filter((item) => item.orderDetails[0].productId !== productId);
        setCarts(carts);
        if (carts?.length == 1 &&  carts[0]?.orderDetails?.length == 0){
          setCarts([]);
        }
        setOrderDetails(carts[0].orderDetails);

        return;
      }
      throw new Error('Failed to remove product from cart');
    } catch (error) {
      console.error('Failed to remove product from cart:', error);
    }
  };

  const handlePay = async () => {
    try {
      const response = await commonAPI.postAPI(`StaffOrder/PromotionInvoice/${carts[0].orderId}`);
      if (response.status === 200) {
        alert('Invoice created successfully!');
        navigate('/invoice');
      } else {
        throw new Error('Failed to create invoice');
      }
    } catch (error) {
      console.error('Failed to create invoice:', error);
      alert('Failed to create invoice');
    }
  };

  if (carts.length === 0) {
    return <h1 className=" h-[55vh] flex justify-center items-center text-4xl">Cart is Empty</h1>;
  }

  return (
    <div className="container mx-auto mt-10">
    <Navbar/>
      <div className="flex-wrap my-10 w-3/4shadow-md">
        <div className="px-10 py-1 bg-white ">
          <div className="flex justify-between pb-8 border-b">
            <h1 className="text-2xl font-semibold">Shopping Cart</h1>
            <h2 className="text-2xl font-semibold">{carts.length} Items</h2>
          </div>
          <div className="flex flex-wrap mt-10 mb-5">
            <h3 className="w-2/5 text-xs font-semibold text-gray-600 uppercase">Product Details</h3>
            <h3 className="w-1/5 text-xs font-semibold text-center text-gray-600 uppercase">Quantity</h3>
            <h3 className="w-1/5 text-xs font-semibold text-center text-gray-600 uppercase">Price</h3>
            <h3 className="w-1/5 text-xs font-semibold text-center text-gray-600 uppercase">Total</h3>
          </div>
          {orderDetails.map((cart) => (
              <div key={cart?.orderDetailId} className="flex items-center px-6 py-5 -mx-8 hover:bg-gray-100">
                <div className="flex w-2/5">
                  <div className="w-20">
                    <img className="h-24" src={cart?.product?.image} alt={cart?.product?.productName} />
                  </div>
                  <div className="flex flex-col justify-between flex-grow ml-4">
                    <span className="text-sm font-bold">OrderID: {cart?.orderId}</span>
                    <span className="text-sm font-bold">{cart?.product?.productName}</span>
                    <div
                      className="text-xs font-semibold text-gray-500 cursor-pointer hover:text-red-500"
                      onClick={() => removeProduct(cart?.productId, cart?.quantity)}
                    >
                      Remove
                    </div>
                  </div>
                </div>
                <span className="w-1/5 text-sm font-semibold text-center">{cart?.quantity}</span>
                <span className="w-1/5 text-sm font-semibold text-center">{cart?.unitPrice}</span>
                <span className="w-1/5 text-sm font-semibold text-center">{(cart?.unitPrice * cart?.quantity)}</span>
              </div>        
          ))}
          <Link to={'/home'} className="flex mt-10 text-sm font-semibold text-gray-900">
            <svg className="w-4 mr-2 text-gray-900 fill-current" viewBox="0 0 448 512">
              <path d="M134.059 296H436c6.627 0 12-5.373 12-12v-56c0-6.627-5.373-12-12-12H134.059v-46.059c0-21.382-25.851-32.09-40.971-16.971L7.029 239.029c-9.373 9.373-9.373 24.569 0 33.941l86.059 86.059c15.119 15.119 40.971 4.411 40.971-16.971V296z" />
            </svg>
            Continue Shopping
          </Link>
        </div>
        <div id="summary" className="container w-2/4 px-8 py-10">
          <div className="mt-8 border-t">
            <button
              onClick={handlePay}
              className="w-full p-2 py-3 text-sm font-semibold uppercase text-hemp bg-bloom"
            >
              Pay
            </button>
          </div>
        </div>
      </div>
      <Footer/>
    </div>
  );
}

export default Cart;
