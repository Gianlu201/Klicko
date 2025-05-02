import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import CouponCard from '../components/CouponCard';
import { toast } from 'sonner';

const CouponPage = () => {
  const backendUrl = import.meta.env.VITE_BACKEND_URL;

  const [availableCoupon, setAvailableCoupon] = useState([]);
  const [unavailableCoupon, setUnavailableCoupon] = useState([]);

  const navigate = useNavigate();

  const getAllCoupons = async () => {
    try {
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
    <div className='max-w-7xl mx-auto min-h-screen px-6 xl:px-0'>
      <h1 className='text-4xl font-bold mt-10 mb-8'>Coupon</h1>

      <h2 className='text-xl font-semibold mb-4'>I tuoi coupon disponibili</h2>
      {availableCoupon.length > 0 ? (
        <div className='grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 mb-6'>
          {availableCoupon.map((coupon) => (
            <CouponCard key={coupon.couponId} coupon={coupon} />
          ))}
        </div>
      ) : (
        <div className='bg-white border border-gray-400/40 rounded-xl mb-6 flex justify-center items-center'>
          <p className='text-gray-500 font-semibold text-lg py-8'>
            Nessun coupon utilizzabile trovato!
          </p>
        </div>
      )}

      {unavailableCoupon.length > 0 && (
        <>
          <h2 className='text-xl font-semibold mb-4'>
            Coupon utilizzati o scaduti
          </h2>
          <div className='grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 mb-6'>
            {unavailableCoupon.map((coupon) => (
              <CouponCard coupon={coupon} />
            ))}
          </div>
        </>
      )}
    </div>
  );
};

export default CouponPage;
