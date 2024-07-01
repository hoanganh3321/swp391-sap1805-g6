import { Link, useNavigate } from "react-router-dom";
import React, { useState } from "react";
import { FiMenu } from "react-icons/fi";
import { AiOutlineShoppingCart } from "react-icons/ai";
import { IoCloseOutline } from "react-icons/io5";
import { MdOutlineNotificationsActive } from "react-icons/md";
import clsx from "clsx";
import axios from "axios";
import {logout} from "../../ultils/helper";

export default function Navbar() {
  const [isSideMenuOpen, setMenu] = useState(false);
  const navigate = useNavigate();

  const navlinks = [
    // {
    //   label: "Sales",
    //   link: "#",
    // },
    // {
    //   label: "Products",
    //   link: "products",
    // },
    // {
    //   label: "About",
    //   link: "#",
    // },
    // {
    //   label: "Contact",
    //   link: "#",
    // },
    {
      label: "Invoice",
      link: "invoice"
    },
    {
      label: "Customer",
      link: "customer"
    }
  ];

  const handleLogout = async () => {
    try {
      await logout();
    } catch (error) {
      console.error('Logout error:', error);
    }
  };

  return (
    <main>
      <nav className="flex items-center justify-between px-8 py-6 ">
        <div className="flex items-center gap-8">
          <section className="flex items-center gap-4">
            {/* menu */}
            <FiMenu
              onClick={() => setMenu(true)}
              className="text-3xl cursor-pointer lg:hidden"
            />
            {/* logo */}
            <Link to={"/home"} className="font-mono text-4xl text-bloom">
              Alumina Store
            </Link>
          </section>
          {navlinks.map((d, i) => (
            <Link
              key={i}
              className="hidden lg:block hover:text-black text-hemp"
              to={`/${d.link}`}
            >
              {d.label}
            </Link>
          ))}
        </div>

        {/* sidebar mobile menu */}
        <div
          className={clsx(
            "fixed h-full w-screen lg:hidden bg-black/50 backdrop-blur-sm top-0 right-0 -translate-x-full transition-all",
            isSideMenuOpen && "translate-x-0"
          )}
        >
          <section className="absolute top-0 left-0 z-50 flex flex-col w-56 h-screen gap-8 p-8 text-black bg-white ">
            <IoCloseOutline
              onClick={() => setMenu(false)}
              className="mt-0 mb-8 text-3xl cursor-pointer"
            />

            {navlinks.map((d, i) => (
              <Link key={i} className="font-bold" to={`/${d.link}`}>
                {d.label}
              </Link>
            ))}
          </section>
        </div>

        <div className="flex items-center">
          {/* <Link to="/cart" className="mr-8 text-2xl cursor-pointer">
            <AiOutlineShoppingCart />
          </Link> */}
          <MdOutlineNotificationsActive className="mr-8 text-2xl cursor-pointer" />
          {/* Nút Logout */}
          <button 
            onClick={handleLogout} 
            className="px-3 py-1 text-lg rounded cursor-pointer text-hemp bg-bloom">
            Logout
          </button>
        </div>
      </nav>
      <hr className="" />
    </main>
  );
}
