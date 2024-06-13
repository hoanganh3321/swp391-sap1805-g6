import React from 'react';
import background1 from '../../assets/img/banner/background.jpg';
import background2 from '../../assets/img/banner/background2.jpg';
import background3 from '../../assets/img/banner/background3.jpg';
import {Link} from 'react-router-dom';

const Hero = () =>{
return (
<div class="mx-auto container flex justify-center items-center py-12 px-4 sm:px-6 2xl:px-0">
            <div class="flex flex-col lg:flex-row justify-center items-center space-y-6 lg:space-y-0">
                <div class="w-80 sm:w-auto flex flex-col justify-start items-start">
                    <div>
                        <p class="text-3xl xl:text-4xl font-semibold leading-9 text-bloom">Jewelry Store</p>
                    </div>
                    <div class="mt-4 lg:w-4/5 xl:w-3/5">
                        <p class="text-base leading-6 text-hemp dark:text-hemp">Lorem ipsum, dolor sit amet consectetur adipisicing elit. Eaque reiciendis inventore iste ratione ex alias quis magni at optio</p>
                    </div>
                    <div class="mt-16 w-full">
                        <button class="px-4 bg-bloom dark:bg-white dark:text-gray-900 flex justify-between items-center w-full lg:w-72 h-14 text-white hover:bg-gray-700 focus:ring-2 outline-none focus:ring-2 focus:ring-offset-2 focus:ring-gray-800 dark:hover:bg-gray-100">
                            <p class="text-xl font-medium leading-5 text-hemp ">See More</p>
                            <svg className="text-hemp" width="32" height="32" viewBox="0 0 32 32" fill="none" xmlns="http://www.w3.org/2000/svg">
                                <path d="M6.66663 16H25.3333" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round" />
                                <path d="M20 21.3333L25.3333 16" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round" />
                                <path d="M20 10.6667L25.3333 16" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round" />
                            </svg>
                        </button>
                    </div>
                </div>
      
                <div class="flex flex-col sm:flex-row jusitfy-center items-center sm:space-x-5 xl:space-x-8 space-y-4 sm:space-y-0">
                    <div class="w-70 sm:w-60">
                        <img class="hidden lg:block" src={background1} alt="sofa" />                       
                    </div>
                    <div class="flex flex-col justify-center items-center space-y-4 sm:space-y-5 lg:space-y-5 xl:space-y-8">
                        <div>
                            <img class="hidden lg:block w-70 sm:w-60" src={background2} alt="chairs" />                           
                        </div>
                        <div>
                            <img class="hidden lg:block w-70 sm:w-60" src={background3} alt="chairs" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
);
};
export default Hero;