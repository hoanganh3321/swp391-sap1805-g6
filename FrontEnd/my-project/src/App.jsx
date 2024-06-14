import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import Home from "../src/components/homepage/Home";
import AdminLayout from "./layouts/admin";
import RtlLayout from "./layouts/rtl";
import Login from "../src/components/login/Login";
import Signup from "../src/components/signup/Signup";
import ProductDetail from "./components/products/ProductDetail";

const App = () => {
  return (
    <Routes>
      <Route path="/" element={<Navigate to="/home" replace />} />
      <Route path="/home" element={<Home />} />
      <Route path="admin/*" element={<AdminLayout />} />
      <Route path="rtl/*" element={<RtlLayout />} />
      <Route path="/login" element={<Login />} />
      <Route path="/signup" element={< Signup />} />
      <Route path="/product/:id" element={<ProductDetail />} />
    </Routes>
  );
};

export default App;
