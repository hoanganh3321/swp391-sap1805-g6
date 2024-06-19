import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import Navbar from "../navbar/Navbar";
import Footer from "../footer/FooterHomePage";
import { useNavigate } from "react-router-dom";

function ProductDetail() {
  const { id } = useParams();
  const [product, setProduct] = useState({});
  const [showAlert, setShowAlert] = useState(false);
  const [showError, setShowError] = useState(false);
  const [quantity, setQuantity] = useState(1);
  const navigate = useNavigate();
  const [customerId, setCustomerId] = useState(1);

  useEffect(() => {
    const fetchProduct = async () => {
      try {
        const response = await fetch(
          `https://localhost:7002/api/product/search/${id}`
        );
        if (!response.ok) {
          throw new Error("Network response was not ok");
        }
        const data = await response.json();
        setProduct(data);
      } catch (error) {
        console.error("Failed to fetch product:", error);
      }
    };
    fetchProduct();
  }, [id]);

  const handleAddToCart = async () => {
    // const token = localStorage.getItem("jwttoken");
    // console.log("JWT Token for Add to Cart:", token); // Debugging line
    try {
      const response = await fetch(
        "https://localhost:7002/api/StaffOrder/addProductToCart",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            customerId: 1, // Replace with actual customerId
            ProductID: product.productId,
            Quantity: quantity, // Use selected quantity
          }),
        }
      );
      if (!response.ok) {
        throw new Error("Failed to add product to cart");
      }
      localStorage.setItem('customerId', customerId);
      // Redirect to /cart after successful addition
      navigate("/cart");
    } catch (error) {
      console.error("Failed to add product to cart:", error);
      setShowError(true);
      setShowAlert(false);
      setTimeout(() => {
        setShowError(false);
      }, 3000);
    }
  };

  if (!Object.keys(product).length) return <div>Product Not Found</div>;

  return (
    <>
      <Navbar />
      <section className="overflow-hidden text-gray-600 body-font">
        <div className="container px-5 py-24 mx-auto">
          <div className="flex flex-wrap mx-auto lg:w-4/5">
            <img
              alt={product?.productName}
              className="lg:w-1/2 w-full lg:h-auto h-64 max-h-[400px] object-contain object-center rounded"
              src={product?.image}
            />
            <div className="w-full mt-6 lg:w-1/2 lg:pl-10 lg:py-6 lg:mt-0">
              <h1 className="mb-1 text-3xl font-medium text-gray-900 title-font">
                {product?.productName}
              </h1>
              <p className="leading-relaxed">{product?.warranty}</p>
              <div className="flex items-center pb-5 mt-6 mb-5 border-b-2 border-gray-100"></div>
              <div className="flex items-center justify-between">
                <span className="text-2xl font-medium text-gray-900 title-font">
                  ${product?.price}
                </span>
                <div className="flex ml-4">
                  <input
                    type="number"
                    value={quantity}
                    onChange={(e) => setQuantity(Number(e.target.value))}
                    min="1"
                    className="w-16 p-2 mr-4 border border-gray-300 rounded"
                  />
                  <button
                    className="flex px-6 py-2 border-0 rounded text-hemp bg-bloom focus:outline-none"
                    onClick={handleAddToCart}
                  >
                    Add to Cart
                  </button>
                </div>
              </div>
              {showAlert && (
                <div className="mt-4 text-green-600">Added to Cart</div>
              )}
              {showError && (
                <div className="mt-4 text-red-600">Failed to add to Cart</div>
              )}
            </div>
          </div>
        </div>
      </section>
      <Footer />
    </>
  );
}

export default ProductDetail;
