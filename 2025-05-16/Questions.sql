/*
You are tasked with building a PostgreSQL-backed database for an EdTech company that manages online training and certification programs for individuals across various technologies.
The goal is to:
Design a normalized schema
Support querying of training data
Ensure secure access
Maintain data integrity and control over transactional updates

Database planning (Nomalized till 3NF)
A student can enroll in multiple courses
Each course is led by one trainer
Students can receive a certificate after passing
Each certificate has a unique serial number
Trainers may teach multiple courses

-- My Approach :

status_master
	status_id, status

learners
	learner_id, name, address, phone, email, status

tutors
	tutor_id, name, qualification, specialization, salary, status

courses
	course_id, course_name, category, tutor_id, duration

enrollment
	enrollment_id, learner_id, course_id, date_of_enrollment, status

certifications
	certificate_id, enrollment_id, date_of_passing, score


-- Mam's Approach :

Tables to Design (Normalized to 3NF):

1. **students**

   * `student_id (PK)`, `name`, `email`, `phone`

2. **courses**

   * `course_id (PK)`, `course_name`, `category`, `duration_days`

3. **trainers**

   * `trainer_id (PK)`, `trainer_name`, `expertise`

4. **enrollmentsnrollment**

   * `enrollment_id (PK)`, `student_id (FK)`, `course_id (FK)`, `enroll_date`

5. **certificates**

   * `certificate_id (PK)`, `enrollment_id (FK)`, `issue_date`, `serial_no`

6. **course\_trainers** (Many-to-Many if needed)

   * `course_id`, `trainer_id`
*/
---

/*
Phase 2: DDL & DML

* Create all tables with appropriate constraints (PK, FK, UNIQUE, NOT NULL)
* Insert sample data using `INSERT` statements
* Create indexes on `student_id`, `email`, and `course_id`

---
*/
CREATE TABLE students (
	student_id SERIAL PRIMARY KEY,
	name VARCHAR(100),
	email VARCHAR(100),
	phone NUMERIC(10) CHECK (Phone > 1000000000)
);

CREATE TABLE courses (
	course_id SERIAL PRIMARY KEY,
	course_name VARCHAR(30),
	category VARCHAR(20),
	duration_days INT CHECK(duration_days> 0)
);

CREATE TABLE trainers (
	trainer_id SERIAL PRIMARY KEY,
	trainer_name VARCHAR(50),
	expertise VARCHAR(20)
);

CREATE TABLE enrollments (
	enrollment_id SERIAL PRIMARY KEY,
	student_id INT,
	course_id INT,
	enroll_date DATE,
	FOREIGN KEY (student_id) REFERENCES students(student_id),
	FOREIGN KEY (course_id) REFERENCES courses(course_id)
);

CREATE TABLE certificates (
	certificate_id SERIAL PRIMARY KEY,
	enrollment_id INT,
	issue_date DATE,
	serial_no VARCHAR(10),
	FOREIGN KEY (ENROLLMENT_ID) REFERENCES enrollments(enrollment_id)
);

CREATE TABLE course_trainer (
	ct_id SERIAL PRIMARY KEY,
	course_id INT,
	trainer_id INT,
	FOREIGN KEY (course_id) REFERENCES courses(course_id),
	FOREIGN KEY (trainer_id) REFERENCES trainers(trainer_id)
)

CREATE INDEX idx_students_student_id ON students(student_id);
CREATE INDEX idx_students_email ON students(email);
CREATE INDEX idx_courses_course_id ON courses(course_id);



INSERT INTO students (name, email, phone) VALUES
('John Doe', 'john.doe@example.com', 9876543210),
('Jane Smith', 'jane.smith@example.com', 9123456789),
('Alex Brown', 'alex.brown@example.com', 9765432109),
('Emma Green', 'emma.green@example.com', 9056781234),
('Liam White', 'liam.white@example.com', 9785432100),
('Olivia Black', 'olivia.black@example.com', 9638527410),
('James Miller', 'james.miller@example.com', 9473625840),
('Charlotte Davis', 'charlotte.davis@example.com', 9856341122);


INSERT INTO courses (course_name, category, duration_days) VALUES
('Python Programming', 'Technology', 30),
('Data Science Basics', 'Technology', 45),
('Digital Marketing', 'Business', 60),
('Web Development', 'Technology', 90),
('Machine Learning', 'Technology', 120),
('Project Management', 'Business', 45),
('Cloud Computing', 'Technology', 60),
('Marketing Strategy', 'Business', 30);


INSERT INTO trainers (trainer_name, expertise) VALUES
('David Williams', 'Python'),
('Sophia Johnson', 'Data Science'),
('Michael Lee', 'Digital Marketing'),
('Rachel Adams', 'Web Development'),
('Ethan Harris', 'Machine Learning'),
('Mia Clark', 'Project Management'),
('Benjamin Wilson', 'Cloud Computing'),
('Ava Turner', 'Marketing Strategy');


INSERT INTO enrollments (student_id, course_id, enroll_date) VALUES
(1, 1, '2025-05-01'),
(2, 2, '2025-05-02'),
(3, 3, '2025-05-03'),
(4, 1, '2025-05-05'),
(5, 3, '2025-05-10'),
(1, 4, '2025-06-01'),
(2, 5, '2025-06-10'),
(3, 2, '2025-06-15'),
(4, 2, '2025-06-20'),
(5, 4, '2025-06-25');


INSERT INTO certificates (enrollment_id, issue_date, serial_no) VALUES
(1, '2025-06-01', 'PY20250001'),
(2, '2025-06-05', 'DS20250001'),
(3, '2025-07-01', 'DM20250001'),
(4, '2025-08-01', 'WD20250001'),
(5, '2025-08-05', 'PM20250001'),
(6, '2025-08-10', 'CC20250001'),
(7, '2025-08-15', 'MS20250001'),
(8, '2025-09-01', 'ML20250001'),
(9, '2025-09-05', 'WD20250002'),
(10, '2025-09-10', 'ML20250002');

INSERT INTO course_trainer (course_id, trainer_id) VALUES
(1, 1),
(2, 2),
(3, 3),
(1, 4),
(2, 5),
(3, 6),
(4, 2),
(5, 1),
(1, 3),
(2, 4),
(4, 5),
(5, 7),
(6, 3),
(7, 2),
(8, 4);

/*
Phase 3: SQL Joins Practice

Write queries to:
*/

-- 1. List students and the courses they enrolled in
SELECT s.student_id, s.name, c.course_id, c.course_name
FROM enrollments e JOIN students s on s.student_id = e.student_id
JOIN courses c ON c.course_id = e.course_id;

-- 2. Show students who received certificates with trainer names
SELECT DISTINCT s.student_id, s.name, c.course_id, t.trainer_name,serial_no as Certificate_Serial_No
FROM certificates cert JOIN enrollments e ON e.enrollment_id = cert.enrollment_id
JOIN students s ON e.student_id = s.student_id
JOIN courses c on c.course_id = e.course_id
JOIN course_trainer ct on c.course_id = ct.course_id
JOIN trainers t on t.trainer_id = ct.trainer_id;

-- 3. Count number of students per course
SELECT c.course_id, c.course_name, COUNT(DISTINCT student_id) FROM
courses c LEFT JOIN enrollments e on e.course_id = c.course_id
GROUP BY c.course_id,c.course_name;

---
/*
Phase 4: Functions & Stored Procedures

*/


-- Function:
-- Create `get_certified_students(course_id INT)`
-- → Returns a list of students who completed the given course and received certificates.

CREATE OR REPLACE FUNCTION fn_get_certified_students(c_id INT)
RETURNS TABLE(student_id INT, name VARCHAR(100), Certificate_No VARCHAR(10)) AS $$
BEGIN
	RETURN QUERY SELECT s.student_id, s.name, serial_no as Certificate
	FROM certificates cert JOIN enrollments e on e.enrollment_id = cert.enrollment_id
	JOIN students s on s.student_id = e.student_id
	WHERE e.course_id = c_id;
END;
$$
LANGUAGE PLPGSQL;

SELECT * FROM fn_get_certified_students(2);

-- Stored Procedure:

-- Create `sp_enroll_student(p_student_id, p_course_id)`
-- → Inserts into `enrollments` and conditionally adds a certificate if completed (simulate with status flag).

CREATE OR REPLACE PROCEDURE proc_enroll_student(p_student_id INT, p_course_id INT, certified CHAR)
AS $$
DECLARE e_id INT;
BEGIN
	INSERT INTO enrollments(student_id, course_id,enroll_date)
	VALUES(p_student_id,p_course_id, CURRENT_DATE)
	RETURNING enrollment_id INTO e_id;

	IF certified = 'Y' or certified = 'y' THEN
		INSERT INTO certificates(enrollment_id, issue_date, serial_no)
		VALUES(e_id,CURRENT_DATE,'CS202500'||e_id::TEXT);
	END IF;
END;
$$
LANGUAGE PLPGSQL;

call proc_enroll_student(1,1,'N');
call proc_enroll_student(1,2,'Y');

SELECT * FROM enrollments;
SELECT * FROM certificates;

---

/*
Phase 5: Cursor

Use a cursor to:
* Loop through all students in a course
* Print name and email of those who do not yet have certificates
*/

DO $$
DECLARE 
	rec record;
	count INT;
	cur CURSOR FOR SELECT * FROM students;
BEGIN
	OPEN cur;

	LOOP 
		FETCH cur INTO rec;
		EXIT WHEN NOT FOUND;
		
		SELECT COUNT(*) INTO count FROM certificates cert 
		JOIN enrollments e on e.enrollment_id = cert.enrollment_id
		WHERE e.student_id = rec.student_id;

		IF count = 0 THEN
			RAISE NOTICE 'ID: %, Name: %, email : %', 
				rec.student_id,rec.name, rec.email;
		END IF;
	END LOOP;
END;
$$;
---

/*
Phase 6: Security & Roles
*/

-- 1. Create a `readonly_user` role:
--    * Can run `SELECT` on `students`, `courses`, and `certificates`
--    * Cannot `INSERT`, `UPDATE`, or `DELETE`

CREATE ROLE readonly_user LOGIN PASSWORD 'readonly';

GRANT CONNECT ON DATABASE edtech TO readonly_user;
GRANT SELECT ON TABLE students,courses,certificates TO readonly_user;
REVOKE INSERT,DELETE,UPDATE ON TABLE students,courses,certificates FROM readonly_user;

-- 2. Create a `data_entry_user` role:
--    * Can `INSERT` into `students`, `enrollments`
--    * Cannot modify certificates directly

CREATE ROLE data_entry_user LOGIN PASSWORD	'data_entry';

GRANT CONNECT ON DATABASE edtech TO data_entry_user;
GRANT INSERT ON TABLE students,enrollments TO data_entry_user;
GRANT USAGE ON SEQUENCE students_student_id_seq, enrollments_enrollment_id_seq TO data_entry_user;
REVOKE ALL PRIVILEGES ON TABLE certificates FROM data_entry_user;
---

SELECT * FROM students;
SELECT * FROM enrollments;
---

/*
Phase 7: Transactions & Atomicity

Write a transaction block that:

* Enrolls a student
* Issues a certificate
* Fails if certificate generation fails (rollback)
*/



-- insert into enrollments
-- insert into certificates
-- COMMIT or ROLLBACK on error

BEGIN TRANSACTION;
DO $$
DECLARE e_id INT;
BEGIN
	INSERT INTO enrollments(student_id, course_id,enroll_date)
	VALUES(10,2, CURRENT_DATE)
	RETURNING enrollment_id INTO e_id;

	INSERT INTO certificates(enrollment_id, issue_date, serial_no)
	VALUES(e_id,CURRENT_DATE,'CS202500'||e_id::TEXT);

	RAISE NOTICE 'SUCCESS';
EXCEPTION WHEN OTHERS THEN
	RAISE NOTICE 'Error: %',SQLERRM;
	ROLLBACK;
	RETURN;
END;
$$;
COMMIT;
---

ABORT;
SELECT * FROM enrollments;

