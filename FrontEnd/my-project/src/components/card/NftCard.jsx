import { IoHeart, IoHeartOutline } from "react-icons/io5";
import { useState } from "react";
import Card from "../card";

const NftCard = ({ index, name, categoryID, price, image, extra }) => {
  const [heart, setHeart] = useState(true);

  return (
    <Card
      key={image}
      extra={`flex flex-col w-full h-full !p-4 3xl:p-![18px] bg-white ${extra}`}
    >
      <div className="w-full h-full">
        <div className="relative w-full">
          <img
            src={image}
            className="w-full h-full mb-3 rounded-xl 3xl:h-full 3xl:w-full"
            alt=""
          />
          <button
            onClick={() => setHeart(!heart)}
            className="absolute flex items-center justify-center p-2 bg-white rounded-full top-3 right-3 text-brand-500 hover:cursor-pointer"
          >
            <div className="flex items-center justify-center w-full h-full text-xl rounded-full hover:bg-gray-50 dark:text-navy-900">
              {heart ? (
                <IoHeartOutline />
              ) : (
                <IoHeart className="text-brand-500" />
              )}
            </div>
          </button>
        </div>

        <div className="flex items-center justify-between px-1 mb-3 md:flex-col md:items-start lg:flex-row lg:justify-between xl:flex-col xl:items-start 3xl:flex-row 3xl:justify-between">
          <div className="mb-2">
            <p className="text-lg font-bold text-navy-700 dark:text-white">
              {" "}
              {name}{" "}
            </p>
            <p className="mt-1 text-sm font-medium text-gray-600 md:mt-2">
              Category : {categoryID?.CategoryName}{" "}
            </p>
          </div>
        </div>

        <div className="flex items-center justify-between md:flex-col md:items-start lg:flex-row lg:justify-between xl:flex-col 2xl:items-start 3xl:flex-row 3xl:items-center 3xl:justify-between">
          <div className="flex">
            <p className="mb-2 ml-12 text-sm font-bold text-brand-500 dark:text-white">
              Current price: {price}
            </p>
          </div>
          <button
            href=""
            className="ml-12 linear rounded-[20px] bg-brand-900 px-4 py-2 text-base font-medium text-white transition duration-200 hover:bg-brand-800 active:bg-brand-700 dark:bg-brand-400 dark:hover:bg-brand-300 dark:active:opacity-90"
          >
            View Detail
          </button>
        </div>
      </div>
    </Card>
  );
};

export default NftCard;
