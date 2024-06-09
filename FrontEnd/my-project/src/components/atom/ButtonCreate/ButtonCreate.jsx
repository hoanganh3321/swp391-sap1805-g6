import React, { useState } from 'react';

const ButtonCreate = ({ isOpen, onClose }) => {
    const [productName, setProductName] = useState('');
    const [productPrice, setProductPrice] = useState('');
    const [weight, setWeight] = useState('');
    const [stoneCost, setStoneCost] = useState('');
    const [warranty, setWarranty] = useState('');
    const [quantity, setQuantity] = useState('');

    const handleSubmit = async (e) => {
        e.preventDefault();

        const productData = {
            productName,
            productPrice,
            weight,
            stoneCost,
            warranty,
            quantity
        };

        try {
            const response = await fetch('https://localhost:7002/api/product/add', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(productData)
            });

            if (response.ok) {
                // Handle successful response
                console.log('Product added successfully');
                // Clear the form
                setProductName('');
                setProductPrice('');
                setWeight('');
                setStoneCost('');
                setWarranty('');
                setQuantity('');
                // Close the modal after submission
                onClose();
            } else {
                // Handle error response
                console.error('Error adding product');
            }
        } catch (error) {
            console.error('Error:', error);
        }
    };

    if (!isOpen) return null;

    return (
        <div className="fixed inset-0 z-50 flex items-center justify-center bg-gray-600 bg-opacity-50">
            <div className="w-full max-w-xl p-6 bg-white rounded-lg shadow-lg">
                <h2 className="mb-4 text-2xl font-semibold">Add Product</h2>
                <form onSubmit={handleSubmit} className="grid grid-cols-2 gap-4">
                    <div className="mb-4">
                        <label className="block mb-2 font-medium text-gray-700">Product Name</label>
                        <input
                            type="text"
                            className="w-full p-3 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
                            value={productName}
                            onChange={(e) => setProductName(e.target.value)}
                            required
                        />
                    </div>
                    <div className="mb-4">
                        <label className="block mb-2 font-medium text-gray-700">Weight</label>
                        <input
                            type="number"
                            className="w-full p-3 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
                            value={weight}
                            onChange={(e) => setWeight(e.target.value)}
                            required
                        />
                    </div>
                    <div className="mb-4">
                        <label className="block mb-2 font-medium text-gray-700">Product Price</label>
                        <input
                            type="number"
                            className="w-full p-3 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
                            value={productPrice}
                            onChange={(e) => setProductPrice(e.target.value)}
                            required
                        />
                    </div>
                    <div className="mb-4">
                        <label className="block mb-2 font-medium text-gray-700">Stone Cost</label>
                        <input
                            type="number"
                            className="w-full p-3 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
                            value={stoneCost}
                            onChange={(e) => setStoneCost(e.target.value)}
                            required
                        />
                    </div>
                    <div className="mb-4">
                        <label className="block mb-2 font-medium text-gray-700">Warranty</label>
                        <input
                            type="text"
                            className="w-full p-3 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
                            value={warranty}
                            onChange={(e) => setWarranty(e.target.value)}
                            required
                        />
                    </div>
                    <div className="mb-4">
                        <label className="block mb-2 font-medium text-gray-700">Quantity</label>
                        <input
                            type="number"
                            className="w-full p-3 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
                            value={quantity}
                            onChange={(e) => setQuantity(e.target.value)}
                            required
                        />
                    </div>
                    <div className="flex justify-end col-span-2">
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
