import { useEffect, useState } from 'react';
import { ChevronLeft, ChevronRight } from 'lucide-react';

export default function Carousel({
  items = [],
  slidesVisible = 3,
  autoplay = true,
  delay = 5000,
  className = '',
}) {
  const [current, setCurrent] = useState(0);

  useEffect(() => {
    if (!autoplay) return;
    const timer = setInterval(() => {
      setCurrent((prev) => (prev + 1) % items.length);
    }, delay);
    return () => clearInterval(timer);
  }, [autoplay, delay, items.length]);

  const nextSlide = () => {
    setCurrent((prev) => (prev + 1) % items.length);
  };

  const prevSlide = () => {
    setCurrent((prev) => (prev - 1 + items.length) % items.length);
  };

  return (
    <div
      className={`relative w-full overflow-hidden rounded-2xl shadow-lg ${className}`}
    >
      <div
        className='flex gap-4 transition-transform duration-700 ease-in-out py-6 px-4'
        style={{
          width: `${(items.length / slidesVisible) * 100}%`,
          transform: `translateX(-${(100 / items.length) * current}%)`,
        }}
      >
        {items.map((item, index) => (
          <div key={index}>
            <img
              src={`https://localhost:7235/uploads/${item.url}`}
              className='w-full h-full rounded-2xl'
            />
          </div>
        ))}
      </div>

      <button
        onClick={prevSlide}
        className='absolute left-4 top-1/2 -translate-y-1/2 bg-white/80 hover:bg-white p-2 rounded-full shadow transition'
      >
        <ChevronLeft className='w-5 h-5 text-gray-700' />
      </button>
      <button
        onClick={nextSlide}
        className='absolute right-4 top-1/2 -translate-y-1/2 bg-white/80 hover:bg-white p-2 rounded-full shadow transition'
      >
        <ChevronRight className='w-5 h-5 text-gray-700' />
      </button>
    </div>
  );
}
