import React from 'react';
import WomanImg from '../../assets/img/banner/woman_hero.png';
import {Link} from 'react-router-dom';

const Hero = () =>{
return (
    <section className='bg-bloom h-[800px] bg-no-repeat bg-cover bg-center mt-4 py-24 bg-hero'>
        <div className='container flex justify-around h-full mx-auto'>
            <div className='flex flex-col justify-center'>
                <div className='flex items-center font-semibold uppercase'>
                    <div className='w-10 h-[2px] bg-hemp mr-3'></div>
                    New Trend
                </div>
                <h1 className='mb-4 text-3xl font-light leading-7'>
                SUMMER SALE <br />
                <span className='font-semibold'>JELWERY</span>
            </h1>
            <Link to={'/home'}
            className='self-start font-semibold uppercase border-b-2 border-hemp'>
            Discover More
            </Link>
            </div>    
            <div className='hidden lg:block'>
                <img src={WomanImg} alt=''/>
            </div>
        </div>
    </section>
);
};
export default Hero;