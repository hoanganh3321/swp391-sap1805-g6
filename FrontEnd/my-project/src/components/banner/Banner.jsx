import React, { useState, useEffect } from "react";
import axios from "axios";

const Banner = () => {
  const [goldPrices, setGoldPrices] = useState([]);

  useEffect(() => {
    const fetchGoldPrices = async () => {
      try {
        const response = await axios.get(
          "https://localhost:7002/api/GoldPriceDisplay/getAllGoldPrice"
        );
        setGoldPrices(response.data);
      } catch (error) {
        console.error("Error fetching gold prices:", error);
      }
    };

    fetchGoldPrices();
  }, []);

  return (
    <div className="min-h-[550px] flex justify-center items-center py-12 sm:py-0">
      <div className="container">
        <div className="grid items-center grid-cols-1 gap-6 sm:grid-cols-2">
          {/* gold price table section */}
          <div data-aos="zoom-in" className="p-6 bg-white rounded-lg shadow-lg">
            <h1 data-aos="fade-up" className="text-3xl font-bold sm:text-4xl text-bloom">
              Gold Price
            </h1>
            <table className="w-full mt-4">
              <thead>
                <tr className="bg-gray-50">
                  <th className="px-4 py-2">DeviceId</th>
                  <th className="px-4 py-2">Location</th>
                  <th className="px-4 py-2">GoldPrice</th>
                </tr>
              </thead>
              <tbody className="text-center">
                {goldPrices.map((goldPrice) => (
                  <tr key={goldPrice.displayId} className="border-b border-gray-200">
                    <td className="px-4 py-2">{goldPrice.deviceId}</td>
                    <td className="px-4 py-2">{goldPrice.location}</td>
                    <td className="px-4 py-2 text-green-500">{goldPrice.goldPrice}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Banner;
