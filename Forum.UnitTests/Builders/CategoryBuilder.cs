using System;
using System.Collections.Generic;
using System.Text;
using Forum.Core.Model;

namespace Forum.UnitTests.Builders
{
    public class CategoryBuilder
    {
        private Category _category;

        public CategoryBuilder()
        {
            _category = new Category(TestName,TestDescription,TestParentId);
        }

        public string TestName => "TestCategory";

        public string TestDescription => "Test Description";

        public int? TestParentId => null;

        public CategoryBuilder Name(string name)
        {
            _category.Name = name;
            return this;
        }

        public CategoryBuilder Description(string description)
        {
            _category.Description = description;
            return this;
        }

        public CategoryBuilder ParentId(int id)
        {
            _category.ParentCategoryID = id;
            return this;
        }

        public CategoryBuilder WithDefaultValues()
        {
            _category = new Category(TestName, TestDescription, TestParentId);
            return this;
        }

        public Category Build() => _category;
    }
}
