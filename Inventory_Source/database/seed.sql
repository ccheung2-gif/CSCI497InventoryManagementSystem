-- Inventory Management System sample data
-- Run schema.sql first, then this file.

PRAGMA foreign_keys = ON;


INSERT INTO CATEGORY (CATEGORY_ID, CATEGORY_NAME) VALUES
    (1, 'Widgets'),
    (2, 'Gadgets'),
    (3, 'Tools'),
    (4, 'Supplies');


INSERT INTO SUPPLIER (SUPPLIER_ID, SUPPLIER_NAME) VALUES
    (1, 'Widget Works Inc.'),
    (2, 'Gadget Source'),
    (3, 'PackRight Supply Co');


INSERT INTO INVENTORY_LOCATION (LOCATION_ID, LOCATION_NAME) VALUES
    (1, 'Main Warehouse'),
    (2, 'Downtown Store'),
    (3, 'Back Room');


-- rows marked "low" are at or below their reorder level, so the Low Stock filter finds something
INSERT INTO PRODUCT (PRODUCT_ID, PRODUCT_NAME, DESCRIPTION, CATEGORY_ID, SUPPLIER_ID, LOCATION_ID, QUANTITY, REORDER_LEVEL, PRICE) VALUES
    (1, 'Standard Widget',       'Basic widget',             1, 1, 2,  15, 20,  4.99),   -- low
    (2, 'Deluxe Widget',         'Heavy-duty widget',        1, 1, 1,  18, 20, 12.99),   -- low
    (3, 'Pocket Gadget',         'Fits in a pocket',         2, 2, 1,  45, 10, 24.50),
    (4, 'Desk Gadget',           'Sits on a desk',           2, 2, 1,   0,  5, 39.99),   -- low (out of stock)
    (5, 'Hex Key Set',           '9-piece metric set',       3, 1, 3,  60, 15,  8.99),
    (6, 'Precision Screwdriver', 'For small screws',         3, 2, 3,   8, 10, 15.75),   -- low
    (7, 'Shipping Box (Small)',  '8 x 6 x 4 in.',            4, 3, 1, 300, 50,  0.75),
    (8, 'Packing Tape',          'Clear, 2 in. wide',        4, 3, 1,  24, 12,  3.25);
