import React, { useState, useEffect } from "react";
import CheckTable from "./components/CheckTable";
import axios from "axios";

const Tables = () => {
  const [tableData, setTableData] = useState([]);
  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get(
          "https://localhost:7002/api/product/list"
        );
        // Map the response data to the required format
        const formattedData = response.data.map((item) => ({
          productname: item.productName,
          weight: item.weight,
          price: item.price,
          quantity: item.quantity,
        }));
        setTableData(formattedData);
      } catch (error) {
        console.error("Error fetching data from API", error);
      }
    };
    fetchData();
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
          <button>Edit</button>
          <button>Delete</button>
        </>
      ),
    }
  ];
  return (
    <div>
      <div className="grid h-full grid-cols-1 gap-5 mt-5">
        <CheckTable columnsData={columnsDataGemstone} tableData={tableData} />
      </div>
    </div>
  );
};

export default Tables;
