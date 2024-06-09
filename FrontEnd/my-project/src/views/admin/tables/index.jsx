import React, { useState, useEffect } from "react";
import CheckTable from "./components/CheckTable";
import axios from "axios";
import EditPopup from "../../../components/atom/EditPopup/EditPopup";

const Tables = () => {
  const [tableData, setTableData] = useState([]);
  const [editData, setEditData] = useState(null);
  const [isEditOpen, setIsEditOpen] = useState(false);

  const fetchTableData = async () => {
    try {
      const response = await axios.get(
        "https://localhost:7002/api/product/list"
      );
      // Map the response data to the required format
      const formattedData = response.data.map((item) => ({
        id: item.id, // Assuming there's an id field in your data
        productName: item.productName,
        weight: item.weight,
        price: item.price,
        quantity: item.quantity,
      }));
      setTableData(formattedData);
    } catch (error) {
      console.error("Error fetching data from API", error);
    }
  };

  // Function to handle delete action
  const handleDelete = async (id) => {
    try {
      await axios.delete(`https://localhost:7002/api/product/delete/${id}`);
      // Refetch data after deletion
      fetchTableData();
    } catch (error) {
      console.error("Error deleting data:", error);
    }
  };

  // Function to handle edit action
  const handleEdit = (id, data) => {
    setEditData(data);  
    setIsEditOpen(true); 
  };

  // Function to handle save after editing
  const handleSaveEdit = async (id, newData) => {
    try {
      await axios.put(`https://localhost:7002/api/product/update/${id}`, newData);
      // Refetch data after update
      fetchTableData();
      setIsEditOpen(false);
    } catch (error) {
      console.error("Error updating data:", error);
    }
  };

  useEffect(() => {
    // Fetch table data when component mounts
    fetchTableData();
  }, []);

  const columnsDataGemstone = [
    { Header: "Product Name", accessor: "productName" },
    { Header: "Weight", accessor: "weight" },
    { Header: "Price", accessor: "price" },
    { Header: "Quantity", accessor: "quantity" },
    {
      Header: "Actions",
      accessor: "actions",
      Cell: ({ row }) => (
        <>
          <button onClick={() => handleEdit(row.values.id, row.original)}>Edit</button>
          <button onClick={() => handleDelete(row.values.id)}>Delete</button>
        </>
      ),
    }
  ];
  
  return (
    <div>
      <div className="grid h-full grid-cols-1 gap-5 mt-5">
        <CheckTable columnsData={columnsDataGemstone} tableData={tableData} />
      </div>
      {isEditOpen && editData && ( // Kiểm tra nếu đang mở bảng popup và có dữ liệu chỉnh sửa
        <EditPopup
          data={editData}
          onSave={(newData) => handleSaveEdit(editData.id, newData)}
          onClose={() => setIsEditOpen(false)}
        />
      )}
    </div>
  );
};

export default Tables;
