import { ClipboardCheck, Clock } from 'lucide-react';
import React from 'react';
import Button from './ui/Button';
import { toast } from 'sonner';

const CouponCard = ({ coupon }) => {
  return (
    <div className='relative bg-white border border-gray-400/40 rounded-xl overflow-hidden'>
      <div className='px-6 py-4'>
        <h3
          className={`text-xl font-medium mb-4 ${
            !coupon.isActive && 'text-gray-600 line-through'
          }`}
        >
          {coupon.code}
        </h3>

        {coupon.isActive ? (
          coupon.expireDate === null ? (
            <p className='text-sm text-gray-500 flex justify-start items-center gap-1.5 mb-3'>
              Nessun limite di tempo
            </p>
          ) : (
            <p className='text-sm text-gray-500 flex justify-start items-center gap-1.5 mb-3'>
              <Clock className='w-4 h-4' />
              Scade il{' '}
              {new Date(coupon.expireDate).toLocaleDateString('it-IT', {
                year: 'numeric',
                month: 'long',
                day: 'numeric',
              })}
            </p>
          )
        ) : (
          <p className='text-sm text-gray-500 flex justify-start items-center gap-1.5 mb-3'>
            <Clock className='w-4 h-4' />
            Coupon non disponibile
          </p>
        )}

        {coupon.percentualSaleAmount > 0 && (
          <p className='text-sm mb-3'>
            Sconto del {coupon.percentualSaleAmount}% sul tuo ordine.
          </p>
        )}

        {coupon.fixedSaleAmount > 0 && (
          <p className='text-sm mb-3'>
            Sconto di {coupon.fixedSaleAmount.toFixed(2).replace('.', ',')}€ sul
            tuo ordine.
          </p>
        )}

        <p className='text-sm '>
          Ordine minimo: {coupon.minimumAmount.toFixed().replace('.', ',')} €
        </p>
      </div>

      {coupon.isActive && (
        <div className='p-4 bg-background border-t border-gray-400/40'>
          <Button
            variant='primary'
            fullWidth={true}
            icon={<ClipboardCheck className='me-3' />}
            onClick={() => {
              navigator.clipboard.writeText(coupon.code);
              toast.info(
                <>
                  <p className='font-bold'>Codice copiato!</p>
                  <p>Hai copiato il codice {coupon.code} negli appunti.</p>
                </>
              );
            }}
          >
            Copia il codice
          </Button>
        </div>
      )}

      {coupon.isActive &&
        (coupon.percentualSaleAmount > 0 ? (
          <span className='absolute top-0 right-0 z-10 w-full transform rotate-45 translate-x-18 -translate-y-4 min-[480px]:translate-x-24 min-[480px]:-translate-y-18 md:translate-x-20 md:-translate-y-12 lg:translate-x-18 lg:-translate-y-12 bg-primary text-white text-sm font-extrabold text-end py-1.5 pe-18 shadow-md'>
            -{coupon.percentualSaleAmount}%
          </span>
        ) : (
          <span className='absolute top-0 right-0 z-10 w-full transform rotate-45 translate-x-18 -translate-y-4 min-[480px]:translate-x-24 min-[480px]:-translate-y-18 md:translate-x-20 md:-translate-y-12 lg:translate-x-18 lg:-translate-y-12 bg-primary text-white text-sm font-extrabold text-end py-1.5 pe-14 shadow-md'>
            -{coupon.fixedSaleAmount.toFixed(2).replace('.', ',')}€
          </span>
        ))}
    </div>
  );
};

export default CouponCard;
