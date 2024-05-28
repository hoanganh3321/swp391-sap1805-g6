import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import Home from "../src/components/homepage/Home";

const App = () => {
  return (
    <Routes>
      <Route path="/" element={<Navigate to="/home" replace />} />
      <Route path="/home" element={<Home/>} />
    </Routes>
  );
};

export default App;
