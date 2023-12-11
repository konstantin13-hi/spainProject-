 SELECT a.id, a.name from accomodations a LEFT JOIN favorites f ON f.accomodation_id = a.id 
 where f.user_id =1;




SELECT a.id, a.name, r.average_rating, a.hasswimmingpool, CASE WHEN CONVERT(varchar(10), GETDATE(), 120) BETWEEN sp.startDate AND sp.endDate THEN a.price * sp.multiplierPrize ELSE a.price END AS price, a.hasgym, a.ispetfriendly, a.hasparking, l.name AS city, l.country, c.name AS category 
                   FROM accomodations a 
                 LEFT JOIN (SELECT a2.id, a2.name, a2.price, a2.hasgym, a2.hasswimmingpool, a2.ispetfriendly, a2.hasparking, AVG(r.pointsAvg) AS average_rating 
                              FROM accomodations a2 
                              LEFT JOIN reviews r ON a2.id = r.accomodation_id 
                              GROUP BY a2.id, a2.name, a2.price, a2.hasgym, a2.hasswimmingpool, a2.ispetfriendly, a2.hasparking) r ON a.id = r.id 
                   LEFT JOIN locations l ON l.id = a.location_id 
                   LEFT JOIN seasonal_prices sp ON a.seasonal_price_id = sp.id 
                   LEFT JOIN categories c ON (c.id = a.category_id) 
                   LEFT JOIN favorites f ON f.accomodation_id = a.id where f.user_id =@userId




SELECT * FROM users;


SELECT * FROM  favorites ;

select * from  categories;
SELECT * FROM seasonal_prices;

SELECT * FROM accomodations;

SELECT a.id, a.name as city, r.average_rating, a.hasswimmingpool,  a.price , a.hasgym, a.ispetfriendly, a.hasparking,l.name as city, l.country ,c.name as category
FROM accomodations a left JOIN 
( SELECT a2.id, a2.name, a2.price, a2.hasgym, a2.hasswimmingpool, a2.ispetfriendly, a2.hasparking, AVG(r.pointsAvg) AS average_rating FROM accomodations a2 
LEFT JOIN reviews r ON a2.id = r.accomodation_id
GROUP BY a2.id, a2.name, a2.price, a2.hasgym, a2.hasswimmingpool, a2.ispetfriendly, a2.hasparking )
r ON a.id = r.id left JOIN locations l ON l.id = a.location_id
left JOIN seasonal_prices sp ON a.seasonal_price_id = sp.id left join categories c on (c.id=a.category_id)




SELECT a.id, a.name, r.average_rating, a.hasswimmingpool, 
  CASE WHEN  CONVERT(varchar(10), GETDATE(), 120) BETWEEN sp.startDate AND sp.endDate THEN a.price * sp.multiplierPrize ELSE a.price END AS price
, a.hasgym, a.ispetfriendly, a.hasparking,l.name as city, l.country ,c.name as category
FROM accomodations a left JOIN 
( SELECT a2.id, a2.name, a2.price, a2.hasgym, a2.hasswimmingpool, a2.ispetfriendly, a2.hasparking, AVG(r.pointsAvg) AS average_rating FROM accomodations a2 
LEFT JOIN reviews r ON a2.id = r.accomodation_id
GROUP BY a2.id, a2.name, a2.price, a2.hasgym, a2.hasswimmingpool, a2.ispetfriendly, a2.hasparking )
r ON a.id = r.id left JOIN locations l ON l.id = a.location_id
left JOIN seasonal_prices sp ON a.seasonal_price_id = sp.id left join categories c on (c.id=a.category_id)
;
