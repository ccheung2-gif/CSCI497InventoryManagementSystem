-- Inventory Management System sample data
-- Fills the empty tables with test data. Run schema.sql first, then this file.
-- IDs are written out so it is easy to see which rows link to which.

PRAGMA foreign_keys = ON;


-- Ten test users (made-up names).
INSERT INTO USER (USER_ID, USERNAME, FIRST_LAST_NAME, ROLE) VALUES
    (1,  'admin.test',   'Test Admin',   'Admin'),
    (2,  'staff.test',   'Test Staff',   'Staff'),
    (3,  'maria.lopez',  'Maria Lopez',  'Admin'),
    (4,  'david.chen',   'David Chen',   'Staff'),
    (5,  'sarah.miller', 'Sarah Miller', 'Staff'),
    (6,  'james.wilson', 'James Wilson', 'Staff'),
    (7,  'aisha.khan',   'Aisha Khan',   'Staff'),
    (8,  'tom.nguyen',   'Tom Nguyen',   'Staff'),
    (9,  'emily.brooks', 'Emily Brooks', 'Staff'),
    (10, 'carlos.diaz',  'Carlos Diaz',  'Staff');


INSERT INTO CATEGORY (CATEGORY_ID, CATEGORY_NAME) VALUES
    (1, 'Widgets'),
    (2, 'Gadgets'),
    (3, 'Tools'),
    (4, 'Supplies');


INSERT INTO SUPPLIER (SUPPLIER_ID, SUPPLIER_NAME, PHONE_NUMBER, EMAIL) VALUES
    (1, 'Widget Works Inc.',   '555-0100', 'orders@widgetworks.example'),
    (2, 'Gadget Source',       '555-0142', 'sales@gadgetsource.example'),
    (3, 'PackRight Supply Co', '555-0177', 'hello@packright.example');


-- Last column is the product's one supplier (see SUPPLIER above).
INSERT INTO PRODUCT (PRODUCT_ID, SKU, PRODUCT_NAME, PRICE, CATEGORY_ID, SUPPLIER_ID) VALUES
    (1, 'WID-001', 'Standard Widget',       4.99,  1, 1),
    (2, 'WID-002', 'Deluxe Widget',         12.99, 1, 1),
    (3, 'GAD-001', 'Pocket Gadget',         24.50, 2, 2),
    (4, 'GAD-002', 'Desk Gadget',           39.99, 2, 2),
    (5, 'TOL-001', 'Hex Key Set',           8.99,  3, 1),
    (6, 'TOL-002', 'Precision Screwdriver', 15.75, 3, 2),
    (7, 'PKG-001', 'Shipping Box (Small)',  0.75,  4, 3),
    (8, 'PKG-002', 'Packing Tape',          3.25,  4, 3);


INSERT INTO INVENTORY_LOCATION (LOCATION_ID, LOCATION_NAME) VALUES
    (1, 'Main Warehouse'),
    (2, 'Downtown Store'),
    (3, 'Back Room');


-- How many of each product are at each location.
-- Rows marked "low" are at or below their reorder level, so low-stock searches find something.
INSERT INTO STOCK_LEVEL (STOCK_ID, PRODUCT_ID, LOCATION_ID, REORDER_LEVEL, QUANTITY) VALUES
    (1,  1, 1, 25, 120),
    (2,  1, 2, 20,  15),   -- low
    (3,  2, 1, 20,  18),   -- low
    (4,  3, 1, 10,  45),
    (5,  4, 1,  5,   0),   -- low (out of stock)
    (6,  5, 3, 15,  60),
    (7,  6, 3, 10,   8),   -- low
    (8,  7, 1, 50, 300),
    (9,  8, 1, 12,  24),
    (10, 8, 2,  5,  10);


-- Stock history. Each starting quantity above arrived as a Receipt,
-- then a few changes happened after that. They add up to STOCK_LEVEL.
INSERT INTO INVENTORY_MOVEMENT (PRODUCT_ID, LOCATION_ID, USER_ID, MOVEMENT_TYPE, QUANTITY, MOVEMENT_DATE) VALUES
    (1, 1, 1, 'Receipt',    130, '2026-10-01 12:00:00'),
    (1, 2, 1, 'Receipt',     25, '2026-10-01 12:00:00'),
    (2, 1, 1, 'Receipt',     18, '2026-10-01 12:00:00'),
    (3, 1, 1, 'Receipt',     45, '2026-10-01 12:00:00'),
    (4, 1, 1, 'Receipt',      2, '2026-10-01 12:00:00'),
    (5, 3, 1, 'Receipt',     60, '2026-10-01 12:00:00'),
    (6, 3, 1, 'Receipt',      8, '2026-10-01 12:00:00'),
    (7, 1, 1, 'Receipt',    300, '2026-10-01 12:00:00'),
    (8, 1, 1, 'Receipt',     34, '2026-10-01 12:00:00'),
    (1, 1, 2, 'Transfer',   -10, '2026-10-15 09:30:00'),   -- 10 widgets moved out of the warehouse...
    (1, 2, 2, 'Transfer',    10, '2026-10-15 09:30:00'),   -- ...and into the store
    (1, 2, 5, 'Issue',      -20, '2026-10-20 14:00:00'),   -- 20 widgets sold at the store
    (4, 1, 7, 'Adjustment',  -2, '2026-10-25 16:00:00'),   -- 2 desk gadgets found damaged
    (8, 1, 4, 'Transfer',   -10, '2026-10-28 10:00:00'),   -- 10 rolls of tape moved to the store
    (8, 2, 4, 'Transfer',    10, '2026-10-28 10:00:00');
