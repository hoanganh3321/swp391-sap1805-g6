import React, { useEffect, useState } from "react";
import { FaStar } from "react-icons/fa";


const TopProducts = ({ handleOrderPopup }) => {
  const [topProducts, setTopProducts] = useState([]);

  useEffect(() => {
    const fetchProducts = async () => {
      try {
        const response = await fetch('https://localhost:7002/api/product/list');
        const data = await response.json();

        // Sort products by stoneCost in descending order and select the top 3
        const sortedProducts = data.sort((a, b) => b.stoneCost - a.stoneCost);
        setTopProducts(sortedProducts.slice(0, 3));
      } catch (error) {
        console.error("Error fetching products:", error);
      }
    };

    fetchProducts();
  }, []);
  return (
    <div>
      <div className="container">
        
        <div className="my-24 ml-10 text-left">
          <p data-aos="fade-up" className="text-2xl text-bloom text-primary">
            Top Rated Products for you
          </p>
          <h1 data-aos="fade-up" className="text-3xl font-bold">
            Best Products
          </h1>
          <p data-aos="fade-up" className="text-xs text-gray-400">
            Lorem ipsum dolor sit amet consectetur, adipisicing elit. Sit
            asperiores modi Sit asperiores modi
          </p>
        </div>
        <div className="grid grid-cols-1 gap-20 mb-10 ml-22 sm:grid-cols-2 md:grid-cols-3 md:gap-5 place-items-center">
          {topProducts.map((product) => (
            <div
              key={product.id}
              data-aos="zoom-in"
              className="rounded-2xl bg-white dark:bg-gray-800 hover:bg-black/80 dark:hover:bg-primary hover:text-white relative shadow-xl duration-300 group max-w-[300px] mt-5"
            >
             
              <div className="h-[100px] w-[300px]">
                <img
                  src={product.image}
                  alt={product.productName}
                  className="max-w-[150px] block mx-auto transform -translate-y-20 group-hover:scale-105 duration-300 drop-shadow-md"
                />
              </div>
              <div className="p-4 text-center">
               
                <div className="flex items-center justify-center w-full gap-1">
                  <FaStar className="text-yellow-500" />
                  <FaStar className="text-yellow-500" />
                  <FaStar className="text-yellow-500" />
                  <FaStar className="text-yellow-500" />
                </div>
                <h1 className="text-xl font-bold group-hover:text-hemp">{product.productName}</h1>
                <p className="text-sm text-gray-500 duration-300 group-hover:text-bloom line-clamp-2">
                  {product.price}
                </p>
                <button
                  className="px-4 py-1 mt-4 duration-300 rounded-full text-hemp bg-primary hover:scale-105 group-hover:bg-bloom group-hover:text-primary"
                  onClick={handleOrderPopup}
                >
                  Order Now
                </button>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default TopProducts;
