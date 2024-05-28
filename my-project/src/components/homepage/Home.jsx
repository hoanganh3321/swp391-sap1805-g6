import React from 'react';
import Navbar from '../../components/navbar/index';
import Footer from '../../components/footer/FooterHomePage';
import Banner from '../../components/banner/Banner';
import Products from '../../components/products/Products';
import TopProducts from '../../components/products/TopProduct';
import Hero from '../../components/banner/Hero';
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

    return (
        <div className="duration-200 bg-white dark:bg-gray-900 dark:text-white">
            <Navbar handleOrderPopup={handleOrderPopup}/>
            <Hero/>
            <Banner handleOrderPopup={handleOrderPopup}/>
            <Products />
            <TopProducts handleOrderPopup={handleOrderPopup}/>
            <Footer />
        </div>
    );
};

export default Home;