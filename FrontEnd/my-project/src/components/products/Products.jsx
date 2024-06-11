import React, { useEffect, useState } from "react";

const Products = () => {
  const [products, setProducts] = useState([]);

  useEffect(() => {
    const fetchProducts = async () => {
      try {
        const response = await fetch('https://localhost:7002/api/product/list');
        const data = await response.json();
        setProducts(data);
      } catch (error) {
        console.error("Error fetching products:", error);
      }
    };
    fetchProducts();
  }, []);

  return (
    <div className="mb-12 mt-14">
      <div className="container">
        
        <div className="text-center mb-10 max-w-[600px] mx-auto">
          <p data-aos="fade-up" className="text-2xl text-primary text-bloom">
            Top Selling Products for you
          </p>
          <h1 data-aos="fade-up" className="text-3xl font-bold">
            Products
          </h1>
          <p data-aos="fade-up" className="text-xs text-gray-400">
            Lorem ipsum dolor sit amet consectetur, adipisicing elit. Sit
            asperiores modi Sit asperiores modi
          </p>
        </div>
        <div>
          <div className="grid grid-cols-1 gap-5 sm:grid-cols-3 md:grid-cols-4 lg:grid-cols-5 place-items-center">
            {products.map((product, index) => (
              <div
                data-aos="fade-up"
                data-aos-delay={index * 200}
                key={product.productId}
                className="space-y-3"
              >
                <div className="h-[260px] w-[260px] overflow-hidden rounded-md">
                  <img
                    src={product.image}
                    alt={product.productName}
                    className="object-cover w-full h-full"
                  />
                </div>
                <div>
                  <h3 className="font-semibold">{product.productName}</h3>
                  <p className="text-sm text-gray-600">Price: ${product.price}</p>
                  <p className="text-sm text-gray-600">Stone Cost: ${product.stoneCost}</p>
                </div>
              </div>
            ))}
          </div>
         
          <div className="flex justify-center">
            <button className="px-5 py-1 mt-10 text-center rounded-md cursor-pointer text-hemp bg-bloom">
              View All Button
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Products;
