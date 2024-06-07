import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import Home from "../src/components/homepage/Home";
import AdminLayout from "./layouts/admin";
import RtlLayout from "./layouts/rtl";


const App = () => {
  return (
    <Routes>
      <Route path="/" element={<Navigate to="/home" replace />} />
      <Route path="/home" element={<Home/>} />
      <Route path="admin/*" element={<AdminLayout />} />
      <Route path="rtl/*" element={<RtlLayout />} />
    </Routes>
  );
};

export default App;
