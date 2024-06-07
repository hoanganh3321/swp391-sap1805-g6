/** @format */
"use client";

import { Link } from "react-router-dom";
import React, { useState } from "react";

import { FiMenu } from "react-icons/fi";
// import { AiOutlineShoppingCart } from "react-icons/ai";
import { IoCloseOutline } from "react-icons/io5";
import clsx from "clsx";

export default function Navbar() {
  const [isSideMenuOpen, setMenu] = useState(false);

  const navlinks = [
    {
      labe: "Collections",
      link: "#"
    },
    {
      labe: "Men",
      link: "#"
    },
    {
      labe: "About",
      link: "#"
    },
    {
      labe: "Contact",
      link: "#"
    }
  ];

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
            <Link href={"/"} className="font-mono text-4xl">
              Alumina Store
            </Link>
          </section>
          {navlinks.map((d, i) => (
            <Link
              key={i}
              className="hidden text-gray-400 lg:block hover:text-black"
              href={d.link}
            >
              {d.labe}
            </Link>
          ))}
        </div>

        {/* sidebar mobile menu */}
        <div
          className={clsx(
            " fixed h-full w-screen lg:hidden bg-black/50  backdrop-blur-sm top-0 right-0  -translate-x-full  transition-all ",
            isSideMenuOpen && "translate-x-0"
          )}
        >
          <section className="absolute top-0 left-0 z-50 flex flex-col w-56 h-screen gap-8 p-8 text-black bg-white ">
            <IoCloseOutline
              onClick={() => setMenu(false)}
              className="mt-0 mb-8 text-3xl cursor-pointer"
            />

            {navlinks.map((d, i) => (
              <Link key={i} className="font-bold" href={d.link}>
                {d.labe}
              </Link>
            ))}
          </section>
        </div>

        {/* last section */}
        {/* <section className="flex items-center gap-4">
          
          <AiOutlineShoppingCart className="text-3xl" />
          <Image
            width={40}
            height={40}
            className="w-8 h-8 rounded-full "
            src="https://i.pravatar.cc/150?img=52"
            alt="avatar-img"
          />
          
        </section> */}
      </nav>
      <hr className="" />
    </main>
  );
}