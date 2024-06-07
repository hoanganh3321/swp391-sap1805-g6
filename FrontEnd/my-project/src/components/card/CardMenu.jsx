import React from "react";
import Dropdown from "../../components/dropdown";
import { AiOutlineUser } from "react-icons/ai";
import { BsThreeDots } from "react-icons/bs";
import { FiSettings } from "react-icons/fi";
import { AiOutlineShop } from "react-icons/ai";
import { TiLightbulb } from "react-icons/ti";
import { Link } from "react-router-dom";

function CardMenu(props) {
  const { transparent } = props;
  const [open, setOpen] = React.useState(false);
  
  return (
    <Dropdown
      button={
        <button
          onClick={() => setOpen(!open)}
          open={open}
          className={`flex items-center text-xl hover:cursor-pointer ${
            transparent
              ? "bg-none text-white hover:bg-none active:bg-none"
              : "bg-lightPrimary p-2 text-brand-500 hover:bg-gray-100 dark:bg-navy-700 dark:text-white dark:hover:bg-white/20 dark:active:bg-white/10"
          } linear justify-center rounded-lg font-bold transition duration-200`}
        >
          <BsThreeDots className="w-6 h-6" />
        </button>
      }
      animation={"origin-top-right transition-all duration-300 ease-in-out"}
      classNames={`${transparent ? "top-8" : "top-11"} right-0 w-max`}
      children={
        <div className="z-50 w-max rounded-xl bg-white py-3 px-4 text-sm shadow-xl shadow-shadow-500 dark:!bg-navy-700 dark:shadow-none">
          {/* <p className="flex items-center gap-2 text-gray-600 cursor-pointer hover:text-black hover:font-medium">
            <span>
              <AiOutlineUser />
            </span>
            <Link to="/admin/create">Create</Link>
           
          </p> */}
          <p className="flex items-center gap-2 pt-1 mt-2 text-gray-600 cursor-pointer hover:text-black hover:font-medium">
            <span>
              <AiOutlineShop />
            </span>
           Edit
          </p>
          <p className="flex items-center gap-2 pt-1 mt-2 text-gray-600 cursor-pointer hover:text-black hover:font-medium">
            <span>
              <TiLightbulb />
            </span>
           Delete
          </p>
          {/* <p className="flex items-center gap-2 pt-1 mt-2 text-gray-600 cursor-pointer hover:text-black hover:font-medium">
            <span>
              <FiSettings />
            </span>
            Panel 4
          </p> */}
        </div>
      }
    />
  );
}

export default CardMenu;
