  
SELECT s.productid, s.customerid, COUNT(1) as amount FROM Sales as s
INNER JOIN 
	( 
		SELECT 
			MIN(salesid) as [id], 
			customerid 
		FROM Sales 
			GROUP BY 
				customerid
	) as firstbuy
	ON firstbuy.id = s.salesid AND firstbuy.customerid = s.customerid
	
GROUP BY s.productid, s.customerid;