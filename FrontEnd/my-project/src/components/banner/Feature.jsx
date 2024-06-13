import React from "react";
import { BiLike } from "react-icons/bi";
import { GrCertificate } from "react-icons/gr";
import { TbTruckDelivery } from "react-icons/tb";
import { GoCommentDiscussion } from "react-icons/go";

const Features = () => {
  return (
    <div className="flex justify-center p-8 mt-5 rounded-lg bg-bloom">
      <div className="flex items-center justify-between space-x-8">
        <div className="flex flex-col items-center text-center">
          <GrCertificate className="w-12 h-12 text-hemp" />
          <h3 className="mt-4 text-lg font-bold text-hemp">
            Certified Authentic
          </h3>
        </div>
        <div className="flex flex-col items-center text-center">
          <BiLike className="w-12 h-12 text-hemp" />
          <h3 className="mt-4 text-lg font-bold text-hemp">
            Lifetime Warranty
          </h3>
        </div>
        <div className="flex flex-col items-center text-center">
          <TbTruckDelivery className="w-12 h-12 text-hemp" />
          <h3 className="mt-4 text-lg font-bold text-hemp">Free Shipping</h3>
        </div>
        <div className="flex flex-col items-center text-center">
          <GoCommentDiscussion className="w-12 h-12 text-hemp" />
          <h3 className="mt-4 text-lg font-bold text-hemp">
            Customer Support
          </h3>
        </div>
      </div>
    </div>
  );
};

export default Features;
