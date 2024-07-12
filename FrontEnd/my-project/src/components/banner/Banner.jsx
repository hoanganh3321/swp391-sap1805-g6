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
      <div className="container flex items-center justify-center">
        <div data-aos="zoom-in" className="w-full max-w-4xl p-8 bg-white rounded-lg shadow-lg">
          <h1 data-aos="fade-up" className="mb-6 text-3xl font-bold sm:text-4xl text-bloom">
            Gold Price
          </h1>
          <table className="w-full mt-4">
            <thead>
              <tr className="bg-gray-50">
                <th className="px-6 py-3 text-lg">DeviceId</th>
                <th className="px-6 py-3 text-lg">Location</th>
                <th className="px-6 py-3 text-lg">GoldPrice</th>
              </tr>
            </thead>
            <tbody className="text-lg text-center">
              {goldPrices.map((goldPrice) => (
                <tr key={goldPrice.displayId} className="border-b border-gray-200">
                  <td className="px-6 py-3">{goldPrice.deviceId}</td>
                  <td className="px-6 py-3">{goldPrice.location}</td>
                  <td className="px-6 py-3 text-green-500">{goldPrice.goldPrice}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
};

export default Banner;
