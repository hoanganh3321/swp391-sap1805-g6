import React, { useEffect, useState } from 'react';
import Footer from '../../components/footer/FooterHomePage';
import Banner from '../../components/banner/Banner';
import Products from '../../components/products/Products';
import TopProducts from '../../components/products/TopProduct';
import Hero from '../../components/banner/Hero';
import Navbar from '../navbar/Navbar';
import Features from '../banner/Feature';
import Blog from '../../components/banner/Blog';
import AOS from "aos";

const Home = () => {
    const [orderPopup, setOrderPopup] = React.useState(false);

  const handleOrderPopup = () => {
    setOrderPopup(!orderPopup);
  };
  React.useEffect(() => {
    AOS.init({
      offset: 100,
      duration: 800,
      easing: "ease-in-sine",
      delay: 100,
    });
    AOS.refresh();
  }, []);

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
        <div className="duration-200 bg-white dark:bg-gray-900 dark:text-white">
            <Navbar/>
            <Hero/>
            <Banner handleOrderPopup={handleOrderPopup}/>
            <div className="flex flex-col w-full mt-20 text-center">
              <h2 className="mb-1 text-xs font-medium tracking-widest text-gray-800 title-font">PRODUCTS</h2>
              <h1 className="text-2xl font-medium text-gray-900 sm:text-3xl title-font">Most Popular Products</h1>
            </div>
            {
              products.length > 0 ? 
              <Products products={products} /> 
              : 
              <div>Loading....</div>
            }
            <Blog/>
            <Features/>
            <TopProducts handleOrderPopup={handleOrderPopup}/>
            <Footer />
        </div>
    );
};

export default Home;