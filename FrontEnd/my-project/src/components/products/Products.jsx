import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

const Products = () => {
  const [products, setProducts] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchProducts = async () => {
      try {
        const response = await fetch('https://localhost:7002/api/product/list');
        if (!response.ok) {
          throw new Error("Network response was not ok");
        }
        const data = await response.json();
        setProducts(data);
      } catch (error) {
        console.error("Error fetching products:", error);
      }
    };
    fetchProducts();
  }, []);

  const handleProductClick = (productId) => {
    navigate(`/product/${productId}`);
  };

  return (
    <div className="mb-12 ml-20 mt-14">
      <div className="container">
        <div>
          <div className="grid grid-cols-1 gap-5 sm:grid-cols-3 md:grid-cols-4 lg:grid-cols-5 place-items-center">
            {products.map((product, index) => (
              <div
                data-aos="fade-up"
                data-aos-delay={index * 200}
                className="space-y-3 transition-transform duration-300 transform cursor-pointer hover:scale-105"
                onClick={() => handleProductClick(product.productId)}
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
        </div>
      </div>
    </div>
  );
};

export default Products;
