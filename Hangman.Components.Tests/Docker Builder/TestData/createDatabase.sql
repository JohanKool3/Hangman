CREATE TABLE IF NOT EXISTS difficulty(
    difficulty_id SERIAL PRIMARY KEY,
    tag VARCHAR(10) NOT NULL UNIQUE,
    description text NOT NULL UNIQUE
);

CREATE TABLE IF NOT EXISTS word(
    word_id SERIAL PRIMARY KEY,
    content VARCHAR(25) NOT NULL,
    difficulty_id int references difficulty(difficulty_id) NOT NULL
);