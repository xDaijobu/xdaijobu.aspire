-- MySql init script

-- NOTE: MySql database and table names are case-sensitive on non-Windows platforms!
--       Column names are always case-insensitive.

-- Create the Catalog table
CREATE TABLE IF NOT EXISTS `user`
(
    `id` int(11) NOT NULL AUTO_INCREMENT,
    `code` varchar(255) NOT NULL,
    `name` varchar(255) NOT NULL,
    `email` varchar(255) NOT NULL,
    PRIMARY KEY (`id`)
);

-- Insert some sample data into the Catalog table only if the table is empty
INSERT INTO user (code, name, email)
SELECT *
FROM (
        SELECT 'CRIS', 'Cristover Wurangian', 'christwurangian@gmail.com' UNION ALL
        SELECT 'SELSA', 'Selsa Pingardi', 'selsa.pingardi@e-intidata.com'
    ) data
-- This clause ensures the rows are only inserted if the table is empty
WHERE NOT EXISTS (SELECT NULL FROM user)