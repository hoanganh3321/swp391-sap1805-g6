import { Link, useNavigate } from "react-router-dom";
import React, { useState } from "react";
import { FiMenu } from "react-icons/fi";
import Dropdown from "../dropdown";
import { IoCloseOutline } from "react-icons/io5";
import { MdOutlineNotificationsActive } from "react-icons/md";
import clsx from "clsx";
import {logout} from "../../ultils/helper";
import avatar from "../../assets/img/avatars/avatar3.png";

export default function Navbar() {
  const [isSideMenuOpen, setMenu] = useState(false);
  const navlinks = [
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
              Blood Diamond
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
          <MdOutlineNotificationsActive className="mr-8 text-2xl cursor-pointer" />
          {/* <button 
            onClick={handleLogout} 
            className="px-3 py-1 text-lg rounded cursor-pointer text-hemp bg-bloom">
            Logout
          </button> */}
          <Dropdown
            button={
              <img
                className="w-10 h-10 rounded-full"
                src={avatar}
                alt="Elon Musk"
              />
            }
            children={
              <div className="flex w-56 flex-col justify-start rounded-[20px] bg-white bg-cover bg-no-repeat shadow-xl shadow-shadow-500 dark:!bg-navy-700 dark:text-white dark:shadow-none">
                <div className="p-4">
                  <div className="flex items-center gap-2">
                    <p className="text-sm font-bold text-navy-700 dark:text-white">
                      👋 Hey, Adela
                    </p>{" "}
                  </div>
                </div>
                <div className="w-full h-px bg-gray-200 dark:bg-white/20 " />

                <div className="flex flex-col p-4">
                  <a
                    onClick={handleLogout}
                    className="mt-3 text-sm font-medium text-red-500 transition duration-150 ease-out hover:text-red-500 hover:ease-in"
                  >
                    Log Out
                  </a>
                </div>
              </div>
            }
            classNames={"py-2 top-8 -left-[180px] w-max"}
          />
        </div>
      </nav>
      <hr className="" />
    </main>
  );
}
