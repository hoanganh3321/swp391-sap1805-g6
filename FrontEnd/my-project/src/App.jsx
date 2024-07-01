import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import Home from "../src/components/homepage/Home";
import AdminLayout from "./layouts/admin";
import RtlLayout from "./layouts/rtl";
import Login from "../src/components/login/Login";
import Signup from "../src/components/signup/Signup";
import ProductDetail from "./components/products/ProductDetail";
import Cart from "./components/Cart/Cart";
import Payment from "./components/paymentpage/Payment";
import LoginAdmin  from "../src/components/login/LoginAdmin";
import Forbidden from "./components/common/Forbidden";
import Customer from "./components/customer/Customer";
import Invoice from "./components/invoice/Invoice";
const ProtectedRoute = ({ element, roleRequired }) => {
  const token = localStorage.getItem('token');
  const roleApp = localStorage.getItem('roleApp');
  if (!token){
    return <Navigate to="/login" replace />;
  }
  if (!roleRequired){
    return element;
  }
  if (roleRequired && roleApp !== roleRequired) {
    return <Navigate to="/403" replace />;
  }

  return element;
};

const App = () => {
  return (
    <Routes>
      <Route path="/" element={<Navigate to="/login" replace />} />
      <Route path="/home" element={<ProtectedRoute element={<Home />} roleRequired="" />}/>
      <Route path="admin/*" element={<ProtectedRoute element={<AdminLayout />} roleRequired="Admin" />} />
      <Route path="rtl/*" element={<ProtectedRoute element={<RtlLayout />} roleRequired="" />} />
      <Route path="/login"  element={<Login />} />
      <Route path="/loginAdmin" element={<LoginAdmin />} />
      <Route path="/signup" element={< Signup />} />
      <Route path="/product/:id" element={<ProtectedRoute element={<ProductDetail />} roleRequired="" />} />
      <Route path="/cart/:customerId" element={<ProtectedRoute element={<Cart />} roleRequired="" />}  />
      <Route path="/payment" element={<ProtectedRoute element={<Payment />} roleRequired="" />}/>
      <Route path="/customer" element={<ProtectedRoute element={<Customer />} roleRequired="" />}/>
      <Route path="/invoice" element={<ProtectedRoute element={<Invoice />} roleRequired="" />}/>
      
      <Route path="/403" element={<Forbidden />} />
    </Routes>
  );
};

export default App;
