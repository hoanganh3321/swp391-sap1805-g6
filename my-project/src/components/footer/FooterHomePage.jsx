import React from "react";
import footerLogo from "../../assets/img/profile/image1.png";
import {
  FaFacebook,
  FaInstagram,
  FaLinkedin,
  FaLocationArrow,
  FaMobileAlt,
} from "react-icons/fa";


const FooterLinks = [
  {
    title: "About",
    link: "/#",
  },
  {
    title: "Reviews",
    link: "/#reviews",
  },
  {
    title: "Try On Box",
    link: "/#tryOnBox",
  },
  {
    title: "Customer Gallery",
    link: "/#customerGallery",
  },
];

const FooterPolocies = [
    {
      title: "Home",
      link: "/#",
    },
    {
      title: "Search",
      link: "/#search",
    },
    {
      title: "Contact",
      link: "/#contact",
    },
    {
      title: "Privacy Policy",
      link: "/#privacyPolicy",
    },
  ];
const Footer = () => {
  return (
    <div className="text-white bg-bloom">
      <div className="container">
        <div data-aos="zoom-in" className="grid pt-5 pb-10 md:grid-cols-3">
          <div className="px-4 py-8">
            <h1 className="flex items-center gap-3 mb-3 text-xl font-bold text-justify sm:text-3xl sm:text-left text-hemp">
              <img src={footerLogo} alt="" className="max-w-[50px]" />
              Alumina Store
            </h1>
            <p>
              Lorem ipsum dolor sit amet consectetur adipisicing elit. Cum in
              beatae ea recusandae blanditiis veritatis.
            </p>
          </div>

          
          <div className="grid grid-cols-2 col-span-2 sm:grid-cols-3 md:pl-10">
            <div>
              <div className="px-4 py-8">
                <h1 className="mb-3 text-xl font-bold text-justify sm:text-xl sm:text-left text-hemp">
                  Quick Links
                </h1>
                <ul className="flex flex-col gap-3">
                  {FooterLinks.map((link) => (
                    <li
                      className="text-gray-200 duration-300 cursor-pointer hover:text-primary hover:translate-x-1"
                      key={link.title}
                    >
                      <span>{link.title}</span>
                    </li>
                  ))}
                </ul>
              </div>
            </div>
            <div>
              <div className="px-4 py-8">
                <h1 className="mb-3 text-xl font-bold text-justify sm:text-xl sm:text-left text-hemp">
                Policies
                </h1>
                <ul className="flex flex-col gap-3">
                  {FooterPolocies.map((link) => (
                    <li
                      className="text-gray-200 duration-300 cursor-pointer hover:text-primary hover:translate-x-1"
                      key={link.title}
                    >
                      <span>{link.title}</span>
                    </li>
                  ))}
                </ul>
              </div>
            </div>
            
            <div>
              <div className="flex items-center gap-3 mt-6">
                <a href="#">
                  <FaInstagram className="text-3xl" />
                </a>
                <a href="#">
                  <FaFacebook className="text-3xl" />
                </a>
                <a href="#">
                  <FaLinkedin className="text-3xl" />
                </a>
              </div>
              <div className="mt-6">
                <div className="flex items-center gap-3">
                  <FaLocationArrow />
                  <p>Thu Duc city</p>
                </div>
                <div className="flex items-center gap-3 mt-3">
                  <FaMobileAlt />
                  <p>+83 123456789</p>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Footer;