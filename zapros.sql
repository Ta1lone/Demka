use hotel_system;
SELECT 
    (SUM(
        CASE 
            WHEN g.`Exit` IS NULL THEN DATEDIFF(CURDATE(), g.`Entry`)
            ELSE DATEDIFF(g.`Exit`, g.`Entry`)
        END
    ) * 100.0) / 
    ( (SELECT COUNT(*) FROM Room_stock) *  -- Всегда 10 номеров
      (DATEDIFF(
          COALESCE( (SELECT MAX(`Exit`) FROM Guests_currently_living_in_the_hotel), CURDATE() ),
          (SELECT MIN(`Entry`) FROM Guests_currently_living_in_the_hotel)
      ) + 1)
    ) AS overall_occupancy_percentage
FROM 
    Guests_currently_living_in_the_hotel g;