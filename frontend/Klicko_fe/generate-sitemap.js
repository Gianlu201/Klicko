import { SitemapStream, streamToPromise } from 'sitemap';
import { writeFileSync } from 'fs';
import { Readable } from 'stream';

const hostname = 'https://klicko.vercel.app';

const productIds = [
  'FF3ED239-E178-4632-8385-042286991C66',
  '6F236570-1625-4190-9A4F-0DA2D0639386',
  'BB36C355-2C8E-4A45-9BE3-151934E2FF4C',
  '0C94EE3C-86F3-4E83-AFB2-2A753416227A',
  '62947BC9-568C-4C34-A8E1-2FB6F05BCA61',
  '8DC3B2F9-850B-42CC-824C-7758112B9370',
  '589ACA9C-2B07-42D2-8920-C4406E5DA977',
  'E25B1044-5049-4CA9-954C-DB76AE235862',
  '81C17E89-5BC3-42BB-9897-DDF27D111440',
  'CEC8F297-D65B-485A-ADC3-F015139CD0C2',
];

const links = [
  { url: '/', changefreq: 'daily', priority: 1.0 },
  { url: '/experiences', changefreq: 'daily', priority: 0.9 },
  { url: '/categories', changefreq: 'weekly', priority: 0.8 },
  { url: '/cart', changefreq: 'daily', priority: 0.8 },
  { url: '/checkout', changefreq: 'daily', priority: 0.8 },
  { url: '/register', changefreq: 'daily', priority: 0.9 },
  { url: '/login', changefreq: 'daily', priority: 0.9 },
  { url: '/dashboard', changefreq: 'daily', priority: 0.8 },
  { url: '/dashboard/orders', changefreq: 'daily', priority: 0.8 },
  { url: '/dashboard/profile', changefreq: 'weekly', priority: 0.7 },
  { url: '/dashboard/experiences', changefreq: 'weekly', priority: 0.8 },
  { url: '/dashboard/admin', changefreq: 'weekly', priority: 0.8 },
  { url: '/dashboard/users', changefreq: 'weekly', priority: 0.8 },
  { url: '/dashboard/settings', changefreq: 'weekly', priority: 0.8 },
  { url: '/redeemVoucher', changefreq: 'weekly', priority: 0.7 },
  { url: '/coupons', changefreq: 'weekly', priority: 0.7 },
  { url: '/loyalty', changefreq: 'monthly', priority: 0.7 },
  { url: '/about', changefreq: 'monthly', priority: 0.5 },
  { url: '/contact', changefreq: 'monthly', priority: 0.5 },
  { url: '/faq', changefreq: 'monthly', priority: 0.5 },
  { url: '/termsAndConditions', changefreq: 'monthly', priority: 0.4 },
  { url: '/privacyPolicy', changefreq: 'monthly', priority: 0.4 },
  ...productIds.map((id) => ({
    url: `/experiences/detail/${id}`,
    changefreq: 'weekly',
    priority: 0.7,
  })),
];

const stream = new SitemapStream({ hostname });
const xml = await streamToPromise(Readable.from(links).pipe(stream));
writeFileSync('./public/sitemap.xml', xml.toString());

console.log('✅ Sitemap generata con successo!');
