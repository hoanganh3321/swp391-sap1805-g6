import React from 'react';

const Features = () => {
  return (
    <div className="p-8 rounded-lg bg-bloom">
      <div className="flex justify-between">
        <div className="flex flex-col items-center">
          <svg className="w-8 h-8 text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M11.049 2.927c.3-.921 1.603-.921 1.902 0l1.519 4.674a1 1 0 001.45 1.069l4.718 1.452a1 1 0 001.069.241l.827 1.79a2 2 0 010 2.828l-1.79.827a1 1 0 00.241 1.069l1.452 4.718a1 1 0 001.069 1.45l4.674 1.519c.921.3.921 1.603 0 1.902l-4.674 1.519a1 1 0 00-1.45 1.069l-1.452 4.718a1 1 0 00-.241 1.069l-1.79.827a2 2 0 01-2.828 0l-.827-1.79a1 1 0 00-1.069-.241l-4.718-1.452a1 1 0 00-1.069-1.45l-1.519-4.674c-.3-.921-1.603-.921-1.902 0l-1.519 4.674a1 1 0 00-1.45 1.069l-4.718 1.452a1 1 0 00-1.069.241l-.827 1.79a2 2 0 010 2.828l1.79.827a1 1 0 00.241 1.069l4.718 1.452a1 1 0 001.069 1.45l1.519 4.674c.921.3.921 1.603 0 1.902l-4.674 1.519a1 1 0 00-1.45 1.069l-1.452 4.718a1 1 0 00-.241 1.069l-1.79.827a2 2 0 01-2.828 0l-.827-1.79a1 1 0 00-1.069-.241l-4.718-1.452a1 1 0 00-1.069-1.45l-1.519-4.674c-.3-.921-1.603-.921-1.902 0z" /></svg>
          <h3 className="mt-4 text-lg font-bold text-white">Certified Authentic</h3>
        </div>
        <div className="flex flex-col items-center">
          <svg className="w-8 h-8 text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M13 7h8m0 0v8m0-8l-8 8-4-4-6 6" /></svg>
          <h3 className="mt-4 text-lg font-bold text-white">Lifetime Warranty</h3>
        </div>
        <div className="flex flex-col items-center">
          <svg className="w-8 h-8 text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 6v6m0 0v6m0-6h6m-6 0H6" /><path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 6v6m0 0v6m0-6h6m-6 0H6" /></svg>
          <h3 className="mt-4 text-lg font-bold text-white">Free Shipping</h3>
        </div>
        <div className="flex flex-col items-center">
          <svg className="w-8 h-8 text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M8 12h.01M12 12h.01M16 12h.01M21 12c0 .552-.007 1.09-.01 1.624v1.624a2 2 0 01-2 2h-16a2 2 0 01-2-2V8.624a2 2 0 012-2h16z" /></svg>
          <h3 className="mt-4 text-lg font-bold text-white">Customer Support</h3>
        </div>
      </div>
    </div>
  );
};

export default Features;