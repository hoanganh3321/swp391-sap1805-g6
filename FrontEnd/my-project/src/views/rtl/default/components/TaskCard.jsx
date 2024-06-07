import CardMenu from "../../../../components/card/CardMenu";
import React from "react";
import Checkbox from "../../../../components/checkbox";
import { MdDragIndicator, MdCheckCircle } from "react-icons/md";
import Card from "../../../../components/card";

const TaskCard = () => {
  return (
    <Card extra="pb-7 p-[20px]">
      {/* task header */}
      <div className="relative flex flex-row justify-between">
        <div className="flex items-center">
          <div className="flex items-center justify-center bg-indigo-100 rounded-full h-9 w-9 dark:bg-indigo-100 dark:bg-white/5">
            <MdCheckCircle className="w-6 h-6 text-brand-500 dark:text-white" />
          </div>
          <h4 className="text-xl font-bold text-navy-700 ms-4 dark:text-white">
            Tasks
          </h4>
        </div>
        <CardMenu />
      </div>

      {/* task content */}

      <div className="w-full h-full">
        <div className="flex items-center justify-between p-2 mt-5">
          <div className="flex items-center justify-center gap-2">
            <Checkbox />
            <p className="text-base font-bold text-navy-700 dark:text-white">
              Landing Page Design
            </p>
          </div>
          <div>
            <MdDragIndicator className="w-6 h-6 text-navy-700 dark:text-white" />
          </div>
        </div>

        <div className="flex items-center justify-between p-2 mt-2">
          <div className="flex items-center justify-center gap-2">
            <Checkbox />
            <p className="text-base font-bold text-navy-700 dark:text-white">
              Mobile App Design
            </p>
          </div>
          <div>
            <MdDragIndicator className="w-6 h-6 text-navy-700 dark:text-white" />
          </div>
        </div>

        <div className="flex items-center justify-between p-2 mt-2">
          <div className="flex items-center justify-center gap-2">
            <Checkbox />
            <p className="text-base font-bold text-navy-700 dark:text-white">
              Dashboard Builder
            </p>
          </div>
          <div>
            <MdDragIndicator className="w-6 h-6 text-navy-700 dark:text-white" />
          </div>
        </div>

        <div className="flex items-center justify-between p-2 mt-2">
          <div className="flex items-center justify-center gap-2">
            <Checkbox />
            <p className="text-base font-bold text-navy-700 dark:text-white">
              Landing Page Design
            </p>
          </div>
          <div>
            <MdDragIndicator className="w-6 h-6 text-navy-700 dark:text-white" />
          </div>
        </div>

        <div className="flex items-center justify-between p-2 mt-2">
          <div className="flex items-center justify-center gap-2">
            <Checkbox />
            <p className="text-base font-bold text-navy-700 dark:text-white">
              Dashboard Builder
            </p>
          </div>
          <div>
            <MdDragIndicator className="w-6 h-6 text-navy-700 dark:text-white" />
          </div>
        </div>
      </div>
    </Card>
  );
};

export default TaskCard;
