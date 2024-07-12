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
            className="object-cover w-full h-full mb-3 rounded-xl 3xl:h-full 3xl:w-full"
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

        <div className="flex flex-col items-center justify-center px-1 mb-3">
          <div className="mb-2">
            <p className="text-lg font-bold text-center text-navy-700 dark:text-white">
              {name}
            </p>
          </div>
        </div>

        <div className="flex flex-col items-center justify-center">
          <div className="flex">
            <p className="text-sm font-bold text-center text-brand-500 dark:text-white">
              Current price: {price}
            </p>
          </div>
        </div>
      </div>
    </Card>
  );
};

export default NftCard;
