import React, { useState } from "react";

const EditPopup = ({ data, onSave, onClose }) => {
  const [editedData, setEditedData] = useState({ ...data });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setEditedData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handleSave = () => {
    onSave(editedData);
  };

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-gray-600 bg-opacity-50">
      <div className="w-full max-w-md p-6 bg-white rounded-lg shadow-lg">
        <h2 className="mb-4 text-2xl font-semibold">Edit Product</h2>
        <form>
          <div className="mb-4">
            <label className="block mb-2 font-medium text-gray-700">Product Name</label>
            <input
              type="text"
              name="productName"
              value={editedData.productName}
              onChange={handleChange}
              className="w-full p-3 mt-1 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
            />
          </div>
          <div className="mb-4">
            <label className="block mb-2 font-medium text-gray-700">Weight</label>
            <input
              type="text"
              name="weight"
              value={editedData.weight}
              onChange={handleChange}
              className="w-full p-3 mt-1 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
            />
          </div>
          <div className="mb-4">
            <label className="block mb-2 font-medium text-gray-700">Price</label>
            <input
              type="text"
              name="price"
              value={editedData.price}
              onChange={handleChange}
              className="w-full p-3 mt-1 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
            />
          </div>
          <div className="mb-4">
            <label className="block mb-2 font-medium text-gray-700">Quantity</label>
            <input
              type="text"
              name="quantity"
              value={editedData.quantity}
              onChange={handleChange}
              className="w-full p-3 mt-1 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
            />
          </div>
          <div className="flex justify-end">
            <button
              type="button"
              className="px-4 py-2 mr-2 transition duration-300 bg-gray-300 rounded hover:bg-gray-400"
              onClick={onClose}
            >
              Cancel
            </button>
            <button
              type="button"
              className="px-4 py-2 text-white transition duration-300 bg-blue-500 rounded hover:bg-blue-600"
              onClick={handleSave}
            >
              Save
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default EditPopup;
