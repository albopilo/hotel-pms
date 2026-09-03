-- Normalize existing guest nationality values to match the nationality dropdown list

-- First, map all variations of Indonesian
UPDATE guests SET nationality = 'Indonesian'
WHERE nationality IS NOT NULL
  AND lower(trim(nationality)) IN (
    'indonesia', 'indonesi', 'indo', 'idnonesia', 'indeonsia',
    'indoensia', 'indoneisa', 'indonsia',
    'aceh', 'aceh tamiang', 'aceh timur',
    'gunung sitoli', 'langkat', 'tanggerang selatan'
  );

-- Malaysia
UPDATE guests SET nationality = 'Malaysian'
WHERE nationality IS NOT NULL
  AND lower(trim(nationality)) = 'malaysia';

-- Kazakhstan
UPDATE guests SET nationality = 'Kazakh'
WHERE nationality IS NOT NULL
  AND lower(trim(nationality)) = 'kazakhstan';

-- Francis (French)
UPDATE guests SET nationality = 'French'
WHERE nationality IS NOT NULL
  AND lower(trim(nationality)) = 'francis';

-- Caines — ambiguous, leave as-is (no confident mapping)
