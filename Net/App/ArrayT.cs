using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysqlT.App
{
    public class ArrayT<T>
    {
        public T[] value { get; private set; }
        private T[] temp = { };
        public ArrayT()
        {
            value = new T[0];
        }
        /// <summary>
        /// 获取指定下标的值
        /// </summary>
        /// <param name="index">从下标0开始</param>
        /// <returns>值</returns>
        public T Get(int index)
        {

            return value[index];
        }
        /// <summary>
        /// 获取一定范围的值
        /// </summary>
        /// <param name="s">起始坐标从0开始</param>
        /// <param name="e">要获取多大</param>
        /// <returns>值数组</returns>
        public T[] Get(int s, int e)
        {
            T[] _t = new T[e];
            for (int i = 0; i < e; i++)
            {
                _t[i] = value[s + i];
            }
            return _t;
        }
        /// <summary>
        /// 添加一个元素到底层
        /// </summary>
        /// <param name="v">将要添加的元素</param>
        public void Add(T v)
        {
            temp = value;

            value = new T[temp.Length == 0 ? 1 : temp.Length + 1];


            for (int i = 0; i < temp.Length; i++)
            {
                value[i] = temp[i];
            }
            value[value.Length - 1] = v;
            temp = null;
        }
        /// <summary>
        /// 添加一个数组到集合
        /// </summary>
        /// <param name="data">数组</param>
        public void AddRange(T[] data)
        {
            if (value == null || value.Length == 0)
            {

                value = data;

            }
            else
            {
                temp = value;

                value = new T[temp.Length == 0 ? data.Length : temp.Length + data.Length];


                for (int i = 0; i < temp.Length; i++)
                {
                    value[i] = temp[i];
                }
                for (int i = 0; i < data.Length; i++)
                {
                    value[temp.Length + i] = data[i];
                }
                temp = null;


            }

        }
        /// <summary>
        /// 获取集合成员数
        /// </summary>
        /// <returns>整数型</returns>
        public int Count()
        {

            return value.Length;
        }
        /// <summary>
        /// 删除一定范围内的元素
        /// </summary>
        /// <param name="s">起始坐标从0开始</param>
        /// <param name="e">要删除的数量</param>
        public void RemoveRange(int s, int e)
        {
            if (s >= 0 && s <= value.Length - e)
            {
                temp = value;
                value = new T[temp.Length - e];
                int last = 0;
                for (int i = 0; i < temp.Length; i++)
                {
                    if (i != s && i > (s + e - 1))
                    {
                        value[last] = temp[i];
                        last++;
                    }
                }
            }
            else
            {

                return;
            }
            temp = null;
        }
        /// <summary>
        /// 搜索一个值返回搜索到的坐标数组
        /// </summary>
        /// <param name="v">将要搜索的值</param>
        /// <returns>整数型数组</returns>
        public int[] ArraySeachint(T v)
        {
            ArrayT<int> t = new ArrayT<int>();
            for (int i = 0; i < value.Length; i++)
            {
                if (v.Equals(value[i]))
                {

                    t.Add(i);
                }

            }
            return t.ToArray();

        }
        /// <summary>
        /// 搜索一个值是否在本集合内存在
        /// </summary>
        /// <param name="data">值</param>
        /// <returns>如果存在返回Ture,反之</returns>
        public bool ArraySeachbool(T data)
        {

            for (int i = 0; i < value.Length; i++)
            {
                if (value[i].Equals(data))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 将集合转化为数组
        /// </summary>
        /// <returns>值数组</returns>
        public T[] ToArray()
        {

            return value;
        }

    }
}
