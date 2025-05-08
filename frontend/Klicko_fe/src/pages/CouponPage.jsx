import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import CouponCard from '../components/CouponCard';
import { toast } from 'sonner';
import CouponsSkeletonLoader from '../components/ui/CouponsSkeletonLoader';

const CouponPage = () => {
  const backendUrl = import.meta.env.VITE_BACKEND_URL;

  const [isLoading, setIsLoading] = useState(true);
  const [availableCoupon, setAvailableCoupon] = useState([]);
  const [unavailableCoupon, setUnavailableCoupon] = useState([]);

  const navigate = useNavigate();

  const getAllCoupons = async () => {
    try {
      setIsLoading(true);

      let tokenObj = localStorage.getItem('klicko_token');

      if (!tokenObj) {
        navigate('/login');
      }

      let token = JSON.parse(tokenObj).token;

      const response = await fetch(`${backendUrl}/Coupon/getAllUserCoupons`, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
          Authorization: `Bearer ${token}`,
        },
      });

      if (response.ok) {
        const data = await response.json();

        setIsLoading(false);
        setAvailableCoupon(data.availableCoupons);
        setUnavailableCoupon(data.unavailableCoupons);
      } else {
        throw new Error('Errore nel recupero dei dati!');
      }
    } catch (e) {
      toast.error(
        <>
          <p className='font-bold'>Errore!</p>
          <p>{e.message}</p>
        </>
      );
      navigate('/');
    }
  };

  useEffect(() => {
    getAllCoupons();
  }, []);

  return (
    <div className='min-h-screen px-6 mx-auto max-w-7xl xl:px-0'>
      <h1 className='mt-10 mb-8 text-4xl font-bold'>Coupon</h1>

      <h2 className='mb-4 text-xl font-semibold'>I tuoi coupon disponibili</h2>

      {isLoading ? (
        <CouponsSkeletonLoader />
      ) : (
        <>
          {availableCoupon.length > 0 ? (
            <div className='grid grid-cols-1 gap-6 mb-6 md:grid-cols-2 lg:grid-cols-3'>
              {availableCoupon.map((coupon) => (
                <CouponCard key={coupon.couponId} coupon={coupon} />
              ))}
            </div>
          ) : (
            <div className='flex items-center justify-center mb-6 bg-white border border-gray-400/40 rounded-xl'>
              <p className='py-8 text-lg font-semibold text-gray-500'>
                Nessun coupon utilizzabile trovato!
              </p>
            </div>
          )}

          {unavailableCoupon.length > 0 && (
            <>
              <h2 className='mb-4 text-xl font-semibold'>
                Coupon utilizzati o scaduti
              </h2>
              <div className='grid grid-cols-1 gap-6 mb-6 md:grid-cols-2 lg:grid-cols-3'>
                {unavailableCoupon.map((coupon) => (
                  <CouponCard key={coupon.couponId} coupon={coupon} />
                ))}
              </div>
            </>
          )}
        </>
      )}
    </div>
  );
};

export default CouponPage;
