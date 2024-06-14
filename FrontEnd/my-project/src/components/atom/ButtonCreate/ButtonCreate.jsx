import React, { useState } from "react";
// import useAuth from "../../../hook/useAth";

const ButtonCreate = ({ isOpen, onClose }) => {
  const [productName, setProductName] = useState("");
  const [barcode, setBarcode] = useState("");
  const [productPrice, setProductPrice] = useState("");
  const [weight, setWeight] = useState("");
  const [stoneCost, setStoneCost] = useState("");
  const [warranty, setWarranty] = useState("");
  const [quantity, setQuantity] = useState("");
  const [manufacturingCost, setManufacturingCost] = useState("");
  const [isBuyback, setIsBuyback] = useState(false);
  const [categoryId, setCategoryId] = useState("");
  const [storeId, setStoreId] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();

    const productData = {
      productName,
      barcode,
      productPrice,
      weight,
      stoneCost,
      warranty,
      quantity,
      manufacturingCost,
      isBuyback,
      categoryId,
      storeId,
    };

    try {
      const response = await fetch("https://localhost:7002/api/product/add", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        //   "Authorization": `Bearer ${token}`,
        },
        body: JSON.stringify(productData),
      });

      if (response.ok) {
        // Handle successful response
        console.log("Product added successfully");
        // Clear the form
        setProductName("");
        setBarcode("");
        setProductPrice("");
        setWeight("");
        setStoneCost("");
        setWarranty("");
        setQuantity("");
        setManufacturingCost("");
        setIsBuyback(false);
        setCategoryId("");
        setStoreId("");
        // Close the modal after submission
        onClose();
      } else {
        // Handle error response
        console.error("Error adding product");
      }
    } catch (error) {
      console.error("Error:", error);
    }
  };

  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-gray-600 bg-opacity-50">
        <div className="w-full max-w-md p-4 bg-white rounded-lg shadow-lg">
            <h2 className="mb-4 text-xl font-semibold">Add Product</h2>
            <form onSubmit={handleSubmit} className="grid grid-cols-2 gap-2">
                <div className="mb-2">
                    <label className="block mb-1 font-medium text-gray-700">Product Name</label>
                    <input
                        type="text"
                        className="w-full p-2 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
                        value={productName}
                        onChange={(e) => setProductName(e.target.value)}
                        required
                    />
                </div>
                <div className="mb-2">
                    <label className="block mb-1 font-medium text-gray-700">Weight</label>
                    <input
                        type="number"
                        className="w-full p-2 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
                        value={weight}
                        onChange={(e) => setWeight(e.target.value)}
                        required
                    />
                </div>
                <div className="mb-2">
                    <label className="block mb-1 font-medium text-gray-700">Product Price</label>
                    <input
                        type="number"
                        className="w-full p-2 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
                        value={productPrice}
                        onChange={(e) => setProductPrice(e.target.value)}
                        required
                    />
                </div>
                <div className="mb-2">
                    <label className="block mb-1 font-medium text-gray-700">Stone Cost</label>
                    <input
                        type="number"
                        className="w-full p-2 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
                        value={stoneCost}
                        onChange={(e) => setStoneCost(e.target.value)}
                        required
                    />
                </div>
                <div className="mb-2">
                    <label className="block mb-1 font-medium text-gray-700">Warranty</label>
                    <input
                        type="text"
                        className="w-full p-2 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
                        value={warranty}
                        onChange={(e) => setWarranty(e.target.value)}
                        required
                    />
                </div>
                <div className="mb-2">
                    <label className="block mb-1 font-medium text-gray-700">Quantity</label>
                    <input
                        type="number"
                        className="w-full p-2 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
                        value={quantity}
                        onChange={(e) => setQuantity(e.target.value)}
                        required
                    />
                </div>
                <div className="mb-2">
                    <label className="block mb-1 font-medium text-gray-700">Manufacturing Cost</label>
                    <input
                        type="number"
                        className="w-full p-2 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
                        value={manufacturingCost}
                        onChange={(e) => setManufacturingCost(e.target.value)}
                        required
                    />
                </div>
                <div className="flex items-center mb-2">
                    <label className="block mb-1 font-medium text-gray-700">Is Buyback</label>
                    <input
                        type="checkbox"
                        className="w-5 h-5 ml-2 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
                        checked={isBuyback}
                        onChange={(e) => setIsBuyback(e.target.checked)}
                    />
                </div>
                <div className="mb-2">
                    <label className="block mb-1 font-medium text-gray-700">Category ID</label>
                    <input
                        type="number"
                        className="w-full p-2 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
                        value={categoryId}
                        onChange={(e) => setCategoryId(e.target.value)}
                        required
                    />
                </div>
                <div className="mb-2">
                    <label className="block mb-1 font-medium text-gray-700">Store ID</label>
                    <input
                        type="number"
                        className="w-full p-2 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
                        value={storeId}
                        onChange={(e) => setStoreId(e.target.value)}
                        required
                    />
                </div>
                <div className="mb-2">
                    <label className="block mb-1 font-medium text-gray-700">Barcode</label>
                    <input
                        type="text"
                        className="w-full p-2 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
                        value={barcode}
                        onChange={(e) => setBarcode(e.target.value)}
                        required
                    />
                </div>
                <div className="flex justify-end col-span-2">
                    <button
                        type="button"
                        className="px-3 py-1 mr-2 transition duration-300 bg-gray-300 rounded hover:bg-gray-400"
                        onClick={onClose}
                    >
                        Cancel
                    </button>
                    <button type="submit" className="px-3 py-1 text-white transition duration-300 bg-blue-500 rounded hover:bg-blue-600">
                        Add
                    </button>
                </div>
            </form>
        </div>
    </div>
);
};

export default ButtonCreate;
