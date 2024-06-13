import React from 'react';
import BlogHomePage from '../../assets/img/banner/blog.png';

const App = () => {
  return (
    <div className="container flex">
      <div className="w-1/2 p-10 mt-32 left-content">
        <h1 className="text-3xl">Blog</h1>
        <p className="mt-3 text-sm">
          If diamonds are a girl's best friend, why can they be so intimidating? A moment in time, a
          memory to be remembered by a uniquely custom diamond creation should be magic and we're
          here to reclaim that; to redefine fine jewelry. From our unique position as diamond suppliers as
          well as personal jewelers, we have access to the most coveted stones to fit any budget. If you can
          dream it, we can make it happen and still exceed your expectations. We're your personal jewelry
          concierge intent on putting the magic back into moments.
        </p>
        <button className="px-4 py-2 mt-5 text-hemp bg-bloom">Learn More</button>
      </div>
      <div className="relative w-1/2 right-content">
        <img src={BlogHomePage} alt="Woman with diamonds" className="w-full h-auto" />
      </div>
    </div>
  );
};

export default App;