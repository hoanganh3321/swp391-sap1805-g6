import React, { useState } from 'react';

const ButtonCreate = ({ isOpen, onClose }) => {
    const [productName, setProductName] = useState('');
    const [productPrice, setProductPrice] = useState('');
    const [weight, setWeight] = useState('');
    const [barcode, setBarcode] = useState('');

    const handleSubmit = (e) => {
        e.preventDefault();
        // Handle the form submission, e.g., send data to an API
        console.log('Product Name:', productName);
        console.log('Barcode:', barcode);
        console.log('Weight:', weight);
        console.log('Product Price:', productPrice);
        // Clear the form
        setProductName('');
        setProductPrice('');
        setWeight('');
        setBarcode('');
        // Close the modal after submission
        onClose();
    };

    if (!isOpen) return null;

    return (
        <div className="fixed inset-0 z-50 flex items-center justify-center bg-gray-600 bg-opacity-50">
            <div className="w-full max-w-md p-6 bg-white rounded-lg shadow-lg">
                <h2 className="mb-4 text-2xl font-semibold">Add Product</h2>
                <form onSubmit={handleSubmit}>
                    <div className="mb-4">
                        <label className="block mb-2 font-medium text-gray-700">Product Name</label>
                        <input
                            type="text"
                            className="w-full p-3 mt-1 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
                            value={productName}
                            onChange={(e) => setProductName(e.target.value)}
                            required
                        />
                    </div>
                    <div className="mb-4">
                        <label className="block mb-2 font-medium text-gray-700">Barcode</label>
                        <input
                            type="text"
                            className="w-full p-3 mt-1 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
                            value={barcode}
                            onChange={(e) => setBarcode(e.target.value)}
                            required
                        />
                    </div>
                    <div className="mb-4">
                        <label className="block mb-2 font-medium text-gray-700">Weight</label>
                        <input
                            type="number"
                            className="w-full p-3 mt-1 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
                            value={weight}
                            onChange={(e) => setWeight(e.target.value)}
                            required
                        />
                    </div>
                    <div className="mb-4">
                        <label className="block mb-2 font-medium text-gray-700">Product Price</label>
                        <input
                            type="number"
                            className="w-full p-3 mt-1 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
                            value={productPrice}
                            onChange={(e) => setProductPrice(e.target.value)}
                            required
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
                        <button type="submit" className="px-4 py-2 text-white transition duration-300 bg-blue-500 rounded hover:bg-blue-600">
                            Add
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default ButtonCreate;
