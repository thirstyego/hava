-- Create the schema
CREATE SCHEMA blog;

-- Create the table for storing blog posts
CREATE TABLE blog.posts (
                            post_id serial PRIMARY KEY,
                            title varchar(255) NOT NULL,
                            body text NOT NULL,
                            created_at timestamp DEFAULT now()
);

-- Create the table for storing comments on blog posts
CREATE TABLE blog.comments (
                               comment_id serial PRIMARY KEY,
                               post_id integer REFERENCES blog.posts(post_id),
                               author varchar(255) NOT NULL,
                               comment text NOT NULL,
                               created_at timestamp DEFAULT now()
);

-- Insert some sample data into the blog posts table
INSERT INTO blog.posts (title, body)
VALUES
    ('My First Post', 'This is the body of my first post.'),
    ('My Second Post', 'This is the body of my second post.'),
    ('My Third Post', 'This is the body of my third post.');

-- Insert some sample data into the comments table
INSERT INTO blog.comments (post_id, author, comment)
VALUES
    (1, 'John Doe', 'This is a comment on the first post.'),
    (1, 'Jane Doe', 'This is another comment on the first post.'),
    (2, 'John Doe', 'This is a comment on the second post.');
