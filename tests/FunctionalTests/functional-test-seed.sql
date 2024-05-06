SET IDENTITY_INSERT Blog ON
INSERT INTO Blog
    ([Id],[Name],[AuthorName_First],[AuthorName_Last])
VALUES
    (1, 'Clean Coder', 'Robert', 'Martin'),
    (2, 'Ardalis', 'Steve', 'Smith'),
    (3, 'CodeOpinion', 'Derek', 'Comartin')
SET IDENTITY_INSERT Blog OFF